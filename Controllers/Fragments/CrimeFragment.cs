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

using CriminalIntent.Model;

namespace CriminalIntent.Controllers.Fragments
{
    public class CrimeFragment : v4.Fragment
    {
        const string C_ARG_CRIME_ID = "crime_id";
        const string C_DIALOG_DATE = "DialogDate";
        const int C_REQUEST_DATE = 0;

        Crime crime;
        EditText titleField;
        Button dateButton;
        CheckBox solvedCheckBox;

        void UpdateDate() => dateButton.Text = crime.Date.ToString();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var crimeId = new Guid(Arguments.GetByteArray(C_ARG_CRIME_ID));
            crime = CrimeLab.Instance(Activity)[crimeId];
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_crime, container, false);

            titleField = view.FindViewById<EditText>(Resource.Id.CrimeTitle);
            titleField.Text = crime.Title;
            titleField.TextChanged += (sender, e) => crime.Title = e.Text.ToString();

            dateButton = view.FindViewById<Button>(Resource.Id.CrimeDate);
            UpdateDate();
            dateButton.Click += (sender, e) =>
            {
                var dialog = DatePickerFragment.NewInstance(crime.Date);
                dialog.SetTargetFragment(this, C_REQUEST_DATE);
                dialog.Show(FragmentManager, C_DIALOG_DATE);
            };

            solvedCheckBox = view.FindViewById<CheckBox>(Resource.Id.CrimeSolved);
            solvedCheckBox.Checked = crime.IsSolved;
            solvedCheckBox.CheckedChange += (sender, e) => crime.IsSolved = e.IsChecked;

            return view;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            var result = (Result)resultCode;
            if (result != Result.Ok)
                return;

            if (requestCode == C_REQUEST_DATE)
            {
                var date = DateTime.FromBinary(data.GetLongExtra(DatePickerFragment.C_EXTRA_DATE, default));
                crime.Date = date;
                UpdateDate();
            }
        }

        public static CrimeFragment NewInstance(Guid crimeId)
        {
            var args = new Bundle();
            args.PutByteArray(C_ARG_CRIME_ID, crimeId.ToByteArray());
            return new CrimeFragment { Arguments = args };
        }
    }
}