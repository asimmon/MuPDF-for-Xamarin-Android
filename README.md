# MuPDF for Xamarin Android

A port of the MuPDF Android library for Xamarin Android. With this library, you will be able to read, write, modify PDF files as well as convert pages to images. It also includes the Android demo activities and classes (MuPDFActivity for example).

## Features

* Create or modify existing PDF files
* Render PDF files as images (jpeg, png, etc.)
* Use existing MuPDF demo activities such as the MuPDFActivity viewer with swipe and zoom
* Fast library thanks to the MuPDF native C library binding for ARMv7 and x86 architectures

From [mupdf.com](http://mupdf.com):

	The renderer in MuPDF is tailored for high quality anti-aliased graphics.
	It renders text with metrics and spacing accurate to within fractions
	of a pixel for the highest fidelity in reproducing the look of a printed page on screen.
	
	MuPDF is also small, fast, and yet complete.
	It supports PDF 1.7 with transparency, encryption, hyperlinks, annotations, searching and more.
	It also reads XPS and OpenXPS documents.
	MuPDF is written modularly, so features can be added on by integrators if they so desire.
	
	Since the 1.2 release of MuPDF, we have optional support
	for interactive features such as form filling, javascript and transitions.

## Get started

[Install the nuget package](https://www.nuget.org/packages/Askaiser.Android.MuPDF/) in the Package Manager Console:

	Install-Package Askaiser.Android.MuPDF

## Rendering PDF as PNG images

Import the `Artifex.MuPdf` (previously `Com.Artifex.Mupdfdemo`) namespace to use these classes.

```C#
var pdf = new MuPDFCore(this, pdfFilepath);
var cookie = new MuPDFCore.Cookie(pdf);
var count = pdf.CountPages();

var screenWidth = 768;
var screenHeight = 1280;
 
for (int i = 0; i < count; i++)
{
    var size = pdf.GetPageSize(i);
 
    int pageWidth = (int)size.X;
    int pageHeight = (int)size.Y;
 
    var bitmap = Bitmap.CreateBitmap(screenWidth, screenHeight, Bitmap.Config.Argb8888);
    pdf.DrawPage(bitmap, i, pageWidth, pageHeight, 0, 0, screenWidth, screenHeight, cookie);
 
    String filename = String.Format("/mnt/sdcard/pdf-{0}.png", i);
    using (var fos = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
    {
        bitmap.Compress(Bitmap.CompressFormat.Png, 100, fos);
    }
}
```

## Using the MuPDFActivity viewer

In order to use the bundled `MuPDFActivity`, you need to update your `AndroidManifest.xml` and declare the activity:

```XML
<!-- "com.artifex.mupdfdemo" is the original package name -->
<activity android:name="com.artifex.mupdfdemo.MuPDFActivity" />
```

For better performances, please enable hardware rendering by adding `android:hardwareAccelerated="true"` in the `<application>` XML node.

Then, in your code, start the activity:

```C#
// pdfPath is the path of the PDF, for example /mnt/sdcard/test.pdf
var intent = new Intent(this, typeof(MuPDFActivity));
intent.SetAction(Intent.ActionView);
intent.SetData(Uri.Parse(pdfPath));

StartActivity(intent);
```

## More documentation and samples

This project is just a Xamarin Android binding of the original MuPDF, so if you want to learn more about the MuPDF API, please look at:

* The [MuPDF Android Java project](https://github.com/asimmon/MuPDF-for-Android) source code (how MuPDFActivity is built, etc.).
* The [MuPDF C project](http://mupdf.com/docs/).

## Licensing

MuPDF is GNU AGPL licenced.

MuPDF is Copyright 2006-2015 Artifex Software, Inc.
