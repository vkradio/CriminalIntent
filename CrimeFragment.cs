﻿using System;
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
        const string C_ARG_CRIME_ID = "crime_id";

        Crime crime;
        EditText titleField;
        Button dateButton;
        CheckBox solvedCheckBox;

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
            dateButton.Text = crime.Date.ToString();
            dateButton.Enabled = false;

            solvedCheckBox = view.FindViewById<CheckBox>(Resource.Id.CrimeSolved);
            solvedCheckBox.Checked = crime.IsSolved;
            solvedCheckBox.CheckedChange += (sender, e) => crime.IsSolved = e.IsChecked;

            return view;
        }

        public static CrimeFragment NewInstance(Guid crimeId)
        {
            var args = new Bundle();
            args.PutByteArray(C_ARG_CRIME_ID, crimeId.ToByteArray());
            return new CrimeFragment { Arguments = args };
        }
    }
}