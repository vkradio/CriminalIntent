using Android.App;
using Android.OS;
using v4 = Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.App;

namespace CriminalIntent
{
    [Activity]
    public class CrimeActivity : SingleFragmentActivity
    {
        protected override v4.Fragment CreateFragment() => new CrimeFragment();
    }
}