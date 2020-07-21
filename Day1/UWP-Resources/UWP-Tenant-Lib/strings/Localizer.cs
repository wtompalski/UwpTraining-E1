using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Windows.ApplicationModel.Resources;

namespace UWP_Tenant_Lib.strings
{
    public class Localizer
    {
        public static string GetTenantString(string key, string tenantName)
        {
            var loader = ResourceLoader.GetForViewIndependentUse($"UWP-Tenant-Lib/resources_{tenantName}");
            var val = loader.GetString(key);
            return val;
        }
    }
}
