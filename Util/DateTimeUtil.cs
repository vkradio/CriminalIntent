using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;

namespace CriminalIntent.Util
{
    public static class DateTimeUtil
    {
        public static Date ToJavaDate(this DateTime dateTime) => new Date(ToMillisFromEpoch(dateTime));

        public static long ToMillisFromEpoch(this DateTime dateTime)
        {
            var dateTimeUtc = dateTime.ToUniversalTime();
            var dateTimeZeroEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)Math.Round((dateTimeUtc - dateTimeZeroEpoch).TotalMilliseconds);
        }
    }
}