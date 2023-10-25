namespace WaterMarker.Interfaces;

public interface IWatermarkResponse
{
    byte[] AsByteArray();
    string AsBase64();
}
