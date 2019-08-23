using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using v4 = Android.Support.V4.App;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CriminalIntent
{
    public class CrimeFragment : v4.Fragment
    {
        Crime crime;
        EditText titleField;
        Button dateButton;
        CheckBox solvedCheckBox;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            crime = new Crime();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var v = inflater.Inflate(Resource.Layout.fragment_crime, container, false);

            titleField = v.FindViewById<EditText>(Resource.Id.CrimeTitle);
            titleField.TextChanged += (sender, e) => crime.Title = e.Text.ToString();

            dateButton = v.FindViewById<Button>(Resource.Id.CrimeDate);
            dateButton.Text = crime.Date.ToString();
            dateButton.Enabled = false;

            solvedCheckBox = v.FindViewById<CheckBox>(Resource.Id.CrimeSolved);
            solvedCheckBox.CheckedChange += (sender, e) => crime.IsSolved = e.IsChecked;

            return v;
        }
    }
}