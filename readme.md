# WaterMarker
WaterMarker is a feature-rich library for embedding watermarks into a multitude of file types such as image, audio, video, and PDF files.
## 📚 Classes and Interfaces
* WatermarkGenerator
An internal class that implements IWatermarkConfig, IWatermarkGenerator, IWatermarkResponse interfaces and manages the main functionality of watermark generation.
* WatermarkHandlerFactory
An internal class implementing IWatermarkHandlerFactory interface. It returns the correct handler for the specific file type.
* FileType
An enum that lists the supported file types for watermarking.
* ExtensionHandler
An internal class implementing IExtentionHandler interface, tasked with identifying the file type from a byte array and validating the watermark input.
## 🚀 Getting Started
To make use of this library, add it to your project and add watermark to your DI.
``` cs
// Register all services of watermark
services.AddWatermark();
```
``` cs
// Inject it into class
private readonly IWatermarkGenerator _watermark;

public ...(IWatermarkGenerator watermark){
    _watermark = watermark;
}
```
## 🖌️ Applying Watermark
To apply a watermark invoke **.OnFile()** method on IWatermarkGenerator object.
Method handles byte arrays and Base64 files.
``` cs
_watermark.OnFile(file)
```
Next you need to pass the file that will be applied as a watermark.
Invoke **.WithFile()** or **.WithDefaultWatermark()**.
``` cs
_watermark.OnFile(file)
          .WithFile(wm)
```
And finally specify the output by calling **.AsByteArray()** or **.AsBase64()**
``` cs
_watermark.OnFile(file)
          .WithFile(wm)
          .AsByteArray();
```
## ✅ Validations
The WaterMarker library has been designed with a set of validations to ensure compatibility between file types and watermark files:
1. Image Watermarks: For video, picture, and PDF files, the library expects the watermark to be in the **.png** format. If a watermark of a different format is attempted to be applied to these file types, an ArgumentException will be thrown.
2. Audio Watermarks: When watermarking audio files, the watermark must be in the **.mp3** format. Any attempt to apply a watermark of a different format to an audio file will result in an ArgumentException.

By adhering to these specific watermark formats, you can ensure the seamless integration of your watermarks into your files. Please note that any deviation from these prescribed formats may result in errors or unsuccessful watermarking.
## ⚙️ Extensibility
The library has been designed with extensibility in mind. You can create your own watermark handlers by implementing the IWatermarkHandler interface, and you can extend support for more file types by extending the FileType enum and modifying the GetHandler method of the WatermarkHandlerFactory class.
## 🔗 Dependencies
This library relies on the MimeDetective package for file type inspection based on the file's content. Please ensure that this package is included in your project dependencies.
## ⚠️ Limitations
While the WaterMarker library offers robust watermarking capabilities, there are a few known limitations:

* File Type Support: The library currently supports a set list of file types enumerated in the FileType enum. If you're dealing with a file type that is not listed, you will need to extend the enum and create a corresponding handler.
* Watermark Formats: Watermarks for audio and image files are expected to be in **.mp3** and **.png** formats respectively. Watermarks in other formats are not currently supported and may lead to errors.
* File Size: While the library should handle files of most sizes, extremely large files might lead to performance issues or errors due to memory limitations.
* MimeDetective Dependency: This library relies on the MimeDetective package to detect file types based on content. As such, any limitations inherent to MimeDetective would also apply to this library.
* Watermark Placement: The current version of the library does not support customizable watermark placement for different file types. Watermarks are embedded following a default strategy specific to each file type.
Please note that despite these limitations, the library offers ample room for customization and extensibility, allowing you to tailor it to suit your specific needs.
## 🖋️ Author
This library was authored and maintained by **Aleksander Burkowski**. For any queries, suggestions or contributions, feel free to reach out.