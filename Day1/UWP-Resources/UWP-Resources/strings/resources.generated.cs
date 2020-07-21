// File generated automatically by ReswPlus. https://github.com/DotNetPlus/ReswPlus
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Data;

namespace UWP_Resources.strings{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("DotNetPlus.ReswPlus", "2.1.2")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public static class resources {
        private static ResourceLoader _resourceLoader;
        static resources()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse("resources");
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("DotNetPlus.ReswPlus", "2.1.2")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class resourcesExtension: MarkupExtension
    {
        public enum KeyEnum
        {
            __Undefined = 0,
        }

        private static ResourceLoader _resourceLoader;
        static resourcesExtension()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse("resources");
        }
        public KeyEnum Key { get; set;}
        public IValueConverter Converter { get; set;}
        public object ConverterParameter { get; set;}
        protected override object ProvideValue()
        {
            string res;
            if(Key == KeyEnum.__Undefined)
            {
                res = "";
            }
            else
            {
                res = _resourceLoader.GetString(Key.ToString());
            }
            return Converter == null ? res : Converter.Convert(res, typeof(String), ConverterParameter, null);
        }
    }
} //UWP_Resources.strings
