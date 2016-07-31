using Android.Runtime;

namespace Artifex.MuPdf
{
    partial class ReaderView
    {
        // We use GetAdapter(...) / SetAdapter created in Metadata.xml
        // instead of the missing Adapter { get; set; }
        protected override Java.Lang.Object RawAdapter
        {
            get { return GetAdapter().JavaCast<Java.Lang.Object>(); }
            set
            {
                var adapter = value.JavaCast<global::Android.Widget.IAdapter>();
                this.SetAdapter(adapter);
            }
        }
    }
}