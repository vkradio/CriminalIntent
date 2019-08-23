using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using v4 = Android.Support.V4.App;
using v7 = Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using ju = Java.Util;

using CriminalIntent.Util;

namespace CriminalIntent
{
    public class DatePickerFragment : v4.DialogFragment
    {
        const string C_ARG_DATE = "date";
        public const string C_EXTRA_DATE = "com.bignerdranch.android.criminalintent.date";

        DatePicker datePicker;

        void SendResult(Result result, DateTime date)
        {
            if (TargetFragment == null)
                return;

            var intent = new Intent();
            intent.PutExtra(C_EXTRA_DATE, date.ToBinary());

            TargetFragment.OnActivityResult(TargetRequestCode, (int)result, intent);
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            var date = DateTime.FromBinary(Arguments.GetLong(C_ARG_DATE));
            //var calendar = ju.Calendar.Instance;
            //calendar.Time = date.ToJavaDate();
            //var year = calendar.Get(ju.CalendarField.Year);
            //var month = calendar.Get(ju.CalendarField.Month);
            //var day = calendar.Get(ju.CalendarField.DayOfMonth);

            var view = LayoutInflater.From(Activity).Inflate(Resource.Layout.dialog_date, null);

            datePicker = view.FindViewById<DatePicker>(Resource.Id.DialogDatePicker);
            //datePicker.Init(year, month, day, null);
            datePicker.Init(date.Year, date.Month - 1, date.Day, null);

            using (var builder = new v7.AlertDialog.Builder(Activity))
            {
                return builder
                    .SetView(view)
                    .SetTitle(Resource.String.DatePickerTitle)
                    .SetPositiveButton(Android.Resource.String.Ok, (sender, e) =>
                    {
                        var pickedDate = new DateTime(datePicker.Year, datePicker.Month + 1, datePicker.DayOfMonth);
                        SendResult(Result.Ok, pickedDate);
                    })
                    .Create();
            }
        }

        public static DatePickerFragment NewInstance(DateTime date)
        {
            var args = new Bundle();
            args.PutLong(C_ARG_DATE, date.ToBinary());
            return new DatePickerFragment { Arguments = args };
        }
    }
}