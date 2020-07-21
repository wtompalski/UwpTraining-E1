// File generated automatically by ReswPlus. https://github.com/DotNetPlus/ReswPlus
using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Data;

namespace UWP_Tenant_Lib.strings{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("DotNetPlus.ReswPlus", "2.1.2")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public static class resources_tenant1 {
        private static ResourceLoader _resourceLoader;
        static resources_tenant1()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse("UWP-Tenant-Lib/resources_tenant1");
        }

        #region Text
        /// <summary>
        ///   Looks up a localized string similar to: default (tenant1-Localizer)
        /// </summary>
        public static string Text
        {
            get
            {
                return _resourceLoader.GetString("Text");
            }
        }
        #endregion
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("DotNetPlus.ReswPlus", "2.1.2")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [MarkupExtensionReturnType(ReturnType = typeof(string))]
    public class resources_tenant1Extension: MarkupExtension
    {
        public enum KeyEnum
        {
            __Undefined = 0,
            Text,
        }

        private static ResourceLoader _resourceLoader;
        static resources_tenant1Extension()
        {
            _resourceLoader = ResourceLoader.GetForViewIndependentUse("UWP-Tenant-Lib/resources_tenant1");
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
} //UWP_Tenant_Lib.strings
