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
using Android.Support.V7.Widget;

namespace CriminalIntent
{
    public class CrimeListFragment : v4.Fragment
    {
        class CrimeHolder : RecyclerView.ViewHolder, View.IOnClickListener
        {
            readonly TextView titleTextView;
            readonly TextView dateTextView;
            readonly ImageView solvedImageView;

            Crime crime;

            public CrimeHolder(LayoutInflater inflater, ViewGroup parent)
                : base(inflater.Inflate(Resource.Layout.list_item_crime, parent, false))
            {
                ItemView.SetOnClickListener(this);

                titleTextView = ItemView.FindViewById<TextView>(Resource.Id.CrimeTitle);
                dateTextView = ItemView.FindViewById<TextView>(Resource.Id.CrimeDate);
                solvedImageView = ItemView.FindViewById<ImageView>(Resource.Id.CrimeSolved);
            }

            public void Bind(Crime crime)
            {
                this.crime = crime;
                titleTextView.Text = this.crime.Title;
                dateTextView.Text = this.crime.Date.ToString();
                solvedImageView.Visibility = crime.IsSolved ? ViewStates.Visible : ViewStates.Gone;
            }

            public void OnClick(View view) => Toast.MakeText(view.Context, $"{crime.Title} clicked!", ToastLength.Short).Show();
        }

        class CrimeAdapter : RecyclerView.Adapter
        {
            readonly List<Crime> crimes;

            public CrimeAdapter(List<Crime> crimes) => this.crimes = crimes;

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var layoutInflater = LayoutInflater.From(parent.Context);
                return new CrimeHolder(layoutInflater, parent);
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var crime = crimes[position];
                ((CrimeHolder)holder).Bind(crime);
            }

            public override int ItemCount => crimes.Count;
        }

        RecyclerView crimeRecyclerView;
        CrimeAdapter adapter;

        void UpdateUi()
        {
            var crimeLab = CrimeLab.Instance(Activity);
            var crimes = crimeLab.Crimes;

            adapter = new CrimeAdapter(crimes);
            crimeRecyclerView.SetAdapter(adapter);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_crime_list, container, false);

            crimeRecyclerView = view.FindViewById<RecyclerView>(Resource.Id.CrimeRecyclerView);
            crimeRecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            UpdateUi();

            return view;
        }
    }
}