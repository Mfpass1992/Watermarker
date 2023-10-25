namespace WaterMarker.Interfaces;

public interface IWatermarkConfig
{
    public IWatermarkResponse WithFile(byte[] file);
    public IWatermarkResponse WithDefaultWatermark();
}
