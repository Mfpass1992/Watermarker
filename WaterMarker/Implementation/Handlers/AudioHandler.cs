using WaterMarker.Enums;
using WaterMarker.Implementation.Tools;
using WaterMarker.Interfaces;

namespace WaterMarker.Implementation.Handlers;

internal class AudioHandler : IWatermarkHandler
{
    public byte[] Handle(byte[] file, byte[]? watermarkFile, FileType type)
    {
        string tempWatermarkFile = "";
        string watermark = ResourcePaths.WatermarkAudio;
        if (watermarkFile != null)
        {
            tempWatermarkFile = ResourcePaths.TempFiles + Guid.NewGuid().ToString() + ".png";
            File.WriteAllBytes(tempWatermarkFile, watermarkFile);
            watermark = tempWatermarkFile;
        }
        string tempFilePath = ResourcePaths.TempFiles + Guid.NewGuid().ToString() + $".{ type.ToString().ToLower() }";

        File.WriteAllBytes(tempFilePath, file);

        byte[] result = FFmpegTools.CombineAudio(watermark, tempFilePath, type);

        File.Delete(tempFilePath);
        if (!String.IsNullOrEmpty(tempWatermarkFile))
            File.Delete(tempWatermarkFile);
        return result;
    }
}
