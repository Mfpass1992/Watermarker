using WaterMarker.Enums;
using WaterMarker.Interfaces;
using MimeDetective;

namespace WaterMarker.Implementation.Tools;

internal class ExtensionHandler : IExtentionHandler
{
    public FileType GetFileType(byte[] file)
    {
        var Inspector = new ContentInspectorBuilder()
        {
            Definitions = MimeDetective.Definitions.Default.All()
        }.Build();

        var result = Inspector.Inspect(file);
        var ext = result.ByFileExtension()[0].Extension;

        return Enum.TryParse(ext, true, out FileType ft)
                    ? ft
                    : FileType.Unknown;
    }

    public void ValidateInputWatermark(FileType type, byte[] file)
    {
        var audio = new List<FileType> { FileType.Mp3, FileType.Mid, FileType.Flac, FileType.Wav };
        var image = new List<FileType> { FileType.Mp4, FileType.M4v, FileType.Mpg, FileType.Pdf, FileType.Gif, FileType.Jpg, FileType.Png, FileType.Jpeg, FileType.Svg };

        FileType inputType = GetFileType(file);

        if (!image.Contains(type) && !audio.Contains(type))
            throw new ArgumentException("Watermark extension is not supported");

        if (audio.Contains(type) && !inputType.Equals(FileType.Mp3))
            throw new ArgumentException("Watermark for specified file needs to be of extension .mp3");

        if (image.Contains(type) && !inputType.Equals(FileType.Png))
            throw new ArgumentException("Watermark for specified file needs to be of extension .png");

    }
}


