using iTextSharp.text;
using iTextSharp.text.pdf;
using WaterMarker.Enums;
using WaterMarker.Interfaces;

namespace WaterMarker.Implementation.Handlers;

internal class PdfHandler : IWatermarkHandler
{
    public byte[] Handle(byte[] file, byte[]? watermarkFile, FileType fileType)
    {
        using (var outputMemoryStream = new MemoryStream())
        {
            var reader = new PdfReader(file);

            var stamper = new PdfStamper(reader, outputMemoryStream);

            byte[] watermarkBytes;
            if (watermarkFile != null)
                watermarkBytes = watermarkFile;
            else
                watermarkBytes = File.ReadAllBytes(ResourcePaths.WatermarkImage);
            

            var watermarkImage = Image.GetInstance(watermarkBytes);
            watermarkImage.ScaleToFit(reader.GetPageSize(1).Width * 0.2f, reader.GetPageSize(1).Height * 0.2f);
            watermarkImage.SetAbsolutePosition(reader.GetPageSize(1).Width - watermarkImage.ScaledWidth - 10, 10);

            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                var page = stamper.GetUnderContent(i);
                page.AddImage(watermarkImage);
            }

            stamper.Close();
            reader.Close();

            return outputMemoryStream.ToArray();
        }
    }
}
