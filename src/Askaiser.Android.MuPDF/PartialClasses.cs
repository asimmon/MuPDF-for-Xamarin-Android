using Android.Runtime;

namespace Com.Artifex.Mupdfdemo
{
    partial class ReaderView
    {
        protected override Java.Lang.Object RawAdapter
        {
            get { return Adapter.JavaCast<Java.Lang.Object>(); }
            set { Adapter = value.JavaCast<global::Android.Widget.IListAdapter>(); }
        }
    }
}