using ImageMagick;
using WaterMarker.Enums;
using WaterMarker.Interfaces;

namespace WaterMarker.Implementation.Handlers;

internal class ImageHandler : IWatermarkHandler
{
    public byte[] Handle(byte[] file, byte[]? watermarkFile, FileType type)
    {
        var image = new MagickImage(file);
        MagickGeometry geometry = new MagickGeometry(image.Width / 5, image.Height / 5);
        geometry.IgnoreAspectRatio = false;

        MagickImage watermark;
        if (watermarkFile != null)
            watermark = new MagickImage(watermarkFile);
        else
            watermark = new MagickImage(ResourcePaths.WatermarkImage);

        watermark.Resize(geometry);
        watermark.Alpha(AlphaOption.On);
        watermark.Evaluate(Channels.Alpha, EvaluateOperator.Divide, 6);

        image.Composite(watermark, Gravity.Southeast, CompositeOperator.Over);

        return image.ToByteArray();
    }
}
