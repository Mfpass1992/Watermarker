using WaterMarker.Enums;

namespace WaterMarker.Interfaces;

internal interface IExtentionHandler
{
    public FileType GetFileType(byte[] file);
    void ValidateInputWatermark(FileType type, byte[] file);
}
