using WaterMarker.Enums;
using WaterMarker.Interfaces;

namespace WaterMarker.Implementation;

internal class WatermarkGenerator : 
    IWatermarkConfig,
    IWatermarkGenerator,
    IWatermarkResponse
{
    private byte[] _currentFile;
    private FileType _type;
    private byte[]? _watermarkFile;
    private readonly IExtentionHandler _handler;
    private readonly IWatermarkHandlerFactory _watermarkHandlerFactory;
    public WatermarkGenerator(IExtentionHandler handler, IWatermarkHandlerFactory watermarkHandlerFactory)
    {
        _handler = handler;
        _watermarkHandlerFactory = watermarkHandlerFactory;
    }
    public byte[] AsByteArray()
    {
        IWatermarkHandler handler = _watermarkHandlerFactory.GetHandler(_type);
        var result = handler.Handle(_currentFile, _watermarkFile, _type);
        return result;
    }
    public string AsBase64()
    {
        IWatermarkHandler handler = _watermarkHandlerFactory.GetHandler(_type);

        var result = handler.Handle(_currentFile, _watermarkFile, _type);
        return Convert.ToBase64String(result);
    }
    public IWatermarkConfig OnFile(byte[] file)
    {
        _currentFile = file;
        _type = _handler.GetFileType(_currentFile);
        return this;
    }

    public IWatermarkConfig OnFile(string Base64File)
    {
        _currentFile = Convert.FromBase64String(Base64File);
        _type = _handler.GetFileType(_currentFile);
        return this;
    }

    public IWatermarkResponse WithFile(byte[] file)
    {
        _handler.ValidateInputWatermark(_type, file);
        _watermarkFile = file;
        return this;
    }

    public IWatermarkResponse WithDefaultWatermark()
    {
        return this;
    }
}
