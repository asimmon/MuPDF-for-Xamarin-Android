# MuPDF for Xamarin Android

A port of the [MuPDF Android library](https://www.mupdf.com/docs/android-sdk.html) for Xamarin Android. With this library, you will be able to read, write, modify PDF files as well as convert pages to images.

The [latest release](https://www.nuget.org/packages/Askaiser.Android.MuPDF/1.18.0) has been created from the 1.18.0 tag of MuPDF source code. It supports the following Android ABIs: `armeabi-v7a`, `arm64-v8a`, `x86` and `x86_64`. Support for `armeabi` and `MIPS` has been removed.

## Features

* Create or modify existing PDF files
* Render PDF files as images (jpeg, png, etc.)
* Anything else coming out of the box from MuPDF library

Quote from [mupdf.com](http://mupdf.com):

> The renderer in MuPDF is tailored for high quality anti-aliased graphics. It renders text with metrics and spacing accurate to within fractions of a pixel for the highest fidelity in reproducing the look of a printed page on screen.
> 
> MuPDF is also small, fast, and yet complete. It supports PDF 1.7 with transparency, encryption, hyperlinks, annotations, searching and more. It also reads XPS and OpenXPS documents. MuPDF is written modularly, so features can be added on by integrators if they so desire.
> 
> Since the 1.2 release of MuPDF, we have optional support for interactive features such as form filling, javascript and transitions.

## Get started

[Install the nuget package](https://www.nuget.org/packages/Askaiser.Android.MuPDF/) in the Package Manager Console:

	Install-Package Askaiser.Android.MuPDF

## Exemple 1: rendering a PDF's first page as an image from its bytes

```C#
using Artifex.MuPdf;

// [...]
var pdfBytes = // ... 

using var doc = Document.OpenDocument(pdfBytes, string.Empty);
using var firstPage = doc.LoadPage(0);

using var pageBitmap = AndroidDrawDevice.DrawPage(firstPage, dpi: 72);
using var jpgStream = new MemoryStream();
await pageBitmap.CompressAsync(Bitmap.CompressFormat.Jpeg, quality: 90, jpgStream);

jpgStream.Seek(0, SeekOrigin.Begin);
var jpgBytes = jpgStream.ToArray();

var downloadsDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads);
var jpgFilePath = System.IO.Path.Combine(downloadsDir.AbsolutePath, "page-0.jpg");

using var fileStream = File.OpenWrite(jpgFilePath);
fileStream.Write(jpgBytes);
```

## Exemple 2: Reading a PDF from a seekable stream

_In order to use input seekable streams, you will need to include the following implementation of the Java input stream to make it compatible with C#._

```C#
using Artifex.MuPdf;

// [...]
using var inputStream = File.OpenRead("...");
using var inputStreamWrapper = new SeekableInputStreamWrapper(inputStream);
using var doc = Document.OpenDocument(inputStreamWrapper, string.Empty);

// [...]
private class SeekableInputStreamWrapper : global::Java.Lang.Object, ISeekableInputStream
{
    private readonly Stream _innerStream;

    public SeekableInputStreamWrapper(Stream innerStream)
    {
        this._innerStream = innerStream;
    }

    public long Position() => this._innerStream.Position;

    public long Seek(long offset, int origin) => this._innerStream.Seek(offset, origin switch
    {
        SeekableStream.SeekCur => SeekOrigin.Current,
        SeekableStream.SeekEnd => SeekOrigin.End,
        SeekableStream.SeekSet => SeekOrigin.Begin,
        _ => SeekOrigin.Begin
    });

    public int Read(byte[] buffer) => this._innerStream.Read(buffer);

    public new void Dispose()
    {
        base.Dispose();
        this.Dispose(true);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        this._innerStream.Dispose();
    }

    // These properties and methods are required by the ISeekableInputStream interface
    public JniManagedPeerStates JniManagedPeerState { get; }

    public void SetJniIdentityHashCode(int value) { }
    public void SetPeerReference(JniObjectReference reference) { }
    public void SetJniManagedPeerState(JniManagedPeerStates value) { }
    public void DisposeUnlessReferenced() { }
    public void Disposed() { }
    public void Finalized() { }
}
```

## Licensing

MuPDF is GNU Affero General Public License v3.0 or later licensed.

MuPDF is Copyright 2006-2020 Artifex Software, Inc.

I am not associated with either Artifex Software, Inc or Xamarin Inc. All rights belong to their respective owners.
