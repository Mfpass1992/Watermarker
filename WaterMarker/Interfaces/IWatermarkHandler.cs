using WaterMarker.Enums;

namespace WaterMarker.Interfaces;

internal interface IWatermarkHandler
{
    public byte[] Handle(byte[] file, byte[]? watermarkFile, FileType fileType);
}
