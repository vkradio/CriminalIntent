using Android.App;
using Android.OS;
using v4 = Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.App;
using Android.Content;
using System;

namespace CriminalIntent
{
    [Activity]
    public class CrimeActivity : SingleFragmentActivity
    {
        const string C_EXTRA_CRIME_ID = "com.bignerdranch.android.criminalintent.crime_id";

        protected override v4.Fragment CreateFragment()
        {
            var crimeId = new Guid(Intent.GetByteArrayExtra(C_EXTRA_CRIME_ID));
            return CrimeFragment.NewInstance(crimeId);
        }

        public static Intent NewIntent(Context packageContext, Guid crimeId)
        {
            var intent = new Intent(packageContext, typeof(CrimeActivity));
            intent.PutExtra(C_EXTRA_CRIME_ID, crimeId.ToByteArray());
            return intent;
        }
    }
}