using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using app4 = Android.Support.V4.App;
using view4 = Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;

using CriminalIntent.Controllers.Activities.Base;
using CriminalIntent.Controllers.Fragments;
using CriminalIntent.Model;

namespace CriminalIntent.Controllers.Activities
{
    [Activity(Label="CrimePagerActivity", Theme="@style/AppTheme")]
    public class CrimePagerActivity : XamarinActivity
    {
        class CrimeStatePagerAdapter : FragmentStatePagerAdapter
        {
            readonly List<Crime> crimes;

            public CrimeStatePagerAdapter(app4.FragmentManager fragmentManager, List<Crime> crimes) : base(fragmentManager) => this.crimes = crimes;

            public override app4.Fragment GetItem(int position)
            {
                var crime = crimes[position];
                return CrimeFragment.NewInstance(crime.Id);
            }

            public override int Count => crimes.Count;
        }

        const string C_EXTRA_CRIME_ID = "com.bignerdranch.android.criminalintent.crime_id";

        view4.ViewPager viewPager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_crime_pager);

            var crimeId = new Guid(Intent.GetByteArrayExtra(C_EXTRA_CRIME_ID));
            var crimes = CrimeLab.Instance(this).Crimes;

            viewPager = FindViewById<view4.ViewPager>(Resource.Id.CrimeViewPager);
            viewPager.Adapter = new CrimeStatePagerAdapter(SupportFragmentManager, crimes);
            viewPager.CurrentItem = crimes
                .Select((c, i) => new { Crime = c, Index = i })
                .Where(c => c.Crime.Id == crimeId)
                .First()
                .Index;
        }

        public static Intent NewIntent(Context packageContext, Guid crimeId)
        {
            var intent = new Intent(packageContext, typeof(CrimePagerActivity));
            intent.PutExtra(C_EXTRA_CRIME_ID, crimeId.ToByteArray());
            return intent;
        }
    }
}