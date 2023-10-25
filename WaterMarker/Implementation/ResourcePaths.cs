using System.Reflection;

namespace WaterMarker.Implementation;

public static class ResourcePaths
{
    private static string _FilesDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName +
        "\\WaterMarker\\Resources\\";
    public static string WatermarkImage
        => _FilesDirectory + @"watermark.png";
    public static string WatermarkAudio
        => _FilesDirectory + @"watermark.mp3";
    public static string TempFiles
        => _FilesDirectory + @"Temp\";
    public static string Output
        => _FilesDirectory + @"Output\";
    public static string FFmpeg
        => _FilesDirectory + @"FFmpeg\ffmpeg.exe";
}
