namespace WaterMarker.Interfaces;

public interface IWatermarkGenerator
{
    public IWatermarkConfig OnFile(byte[] file);
    public IWatermarkConfig OnFile(string Base64File);
}
