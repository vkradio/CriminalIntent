using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using v4 = Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CriminalIntent.Controllers.Activities.Base
{
    public abstract class SingleFragmentActivity : XamarinActivity
    {
        protected abstract v4.Fragment CreateFragment();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_fragment);

            var fragment = SupportFragmentManager.FindFragmentById(Resource.Id.FragmentContainer);
            if (fragment == null)
            {
                fragment = CreateFragment();
                SupportFragmentManager
                    .BeginTransaction()
                    .Add(Resource.Id.FragmentContainer, fragment)
                    .Commit();
            }
        }
    }
}