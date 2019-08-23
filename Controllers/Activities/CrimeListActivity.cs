using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using v4 = Android.Support.V4.App;
using Android.Views;
using Android.Widget;

using CriminalIntent.Controllers.Activities.Base;
using CriminalIntent.Controllers.Fragments;

namespace CriminalIntent.Controllers.Activities
{
    [Activity(Label="@string/app_name", Theme="@style/AppTheme", MainLauncher=true)]
    public class CrimeListActivity : SingleFragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        }

        protected override v4.Fragment CreateFragment() => new CrimeListFragment();
    }
}