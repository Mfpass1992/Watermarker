using System.Diagnostics;
using WaterMarker.Enums;

namespace WaterMarker.Implementation.Tools;

public static class FFmpegTools
{
    public static byte[] CombineAudio(string watermark, string input, FileType type)
    {
        string outputPath = ResourcePaths.Output + Guid.NewGuid().ToString() + ".mp3";

        string command = $"-i \"{watermark}\" -i \"{input}\" " +
            $"-filter_complex \"[0:a]aformat=sample_fmts=s16p:sample_rates=44100:channel_layouts=stereo[a1];[1:a]aformat=sample_fmts=s16p:sample_rates=44100:channel_layouts=stereo[a2];[a1][a2]concat=n=2:v=0:a=1[out]\" " +
            $"-map \"[out]\" -c:a libmp3lame -q:a 2 \"{outputPath}\"";

        ExecuteFFmpegCommand(command);
        outputPath = ConvertToFormat(outputPath, type);

        var result = File.ReadAllBytes(outputPath);
        File.Delete(outputPath);
        return result;
    }

    public static byte[] AddImageToVideo(string watermark, string input, FileType type)
    {
        string outputPath = ResourcePaths.Output + Guid.NewGuid().ToString() + ".mp4";

        string command = $"-i \"{input}\" -i \"{watermark}\" " +
            $"-filter_complex \"[1]scale=iw*0.2:-1[logo];[0][logo]overlay=5:H-h-5:format=auto,format=yuv420p\" " +
            $"-c:a copy \"{outputPath}\"";

        ExecuteFFmpegCommand(command);
        outputPath = ConvertToFormat(outputPath, type);

        var result = File.ReadAllBytes(outputPath);
        File.Delete(outputPath);
        return result;
    }

    private static string ConvertToFormat(string outputPath, FileType type)
    {
        var oldOutputPath = outputPath;
        outputPath.Substring(0, outputPath.Length - 4);
        outputPath += $".{type.ToString().ToLower()}";
        string command = $"-i \"{oldOutputPath}\" \"{outputPath}\"";
        ExecuteFFmpegCommand(command);
        File.Delete(oldOutputPath);
        return outputPath;
    }
    private static void ExecuteFFmpegCommand(string command)
    {
        ProcessStartInfo ffmpegInfo = new ProcessStartInfo();
        ffmpegInfo.FileName = ResourcePaths.FFmpeg;
        ffmpegInfo.Arguments = command;
        ffmpegInfo.WorkingDirectory = Path.GetDirectoryName(ResourcePaths.FFmpeg);
        ffmpegInfo.RedirectStandardOutput = true;
        ffmpegInfo.RedirectStandardError = true;
        ffmpegInfo.UseShellExecute = false;
        ffmpegInfo.CreateNoWindow = true;

        using (Process process = new Process())
        {
            process.StartInfo = ffmpegInfo;
            process.Start();

            process.BeginOutputReadLine();
            string tmpErrorOut = process.StandardError.ReadToEnd();

            process.WaitForExit();

            if (process.ExitCode == 1)
            {
                throw new Exception(tmpErrorOut);
            }
        }
    }
}
