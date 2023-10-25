using WaterMarker.Enums;

namespace WaterMarker.Interfaces;

internal interface IWatermarkHandlerFactory
{
    public IWatermarkHandler GetHandler(FileType type);
}
