using WaterMarker.Enums;
using WaterMarker.Implementation.Handlers;
using WaterMarker.Interfaces;

namespace WaterMarker.Implementation;

internal class WatermarkHandlerFactory : IWatermarkHandlerFactory
{
    public WatermarkHandlerFactory() { }
    public IWatermarkHandler GetHandler(FileType type)
    {
        var audio = new List<FileType> { FileType.Mp3, FileType.Mid, FileType.Flac, FileType.Wav };
        var video = new List<FileType> { FileType.Mp4, FileType.M4v, FileType.Mpg };
        var image = new List<FileType> { FileType.Gif, FileType.Jpg, FileType.Png };
        var pdf = new List<FileType> { FileType.Pdf };
        if (audio.Contains(type))
            return new AudioHandler();
        if (video.Contains(type))
            return new VideoHandler();
        if (image.Contains(type))
            return new ImageHandler();
        if (pdf.Contains(type))
            return new PdfHandler();
        throw new Exception();
    }
}
