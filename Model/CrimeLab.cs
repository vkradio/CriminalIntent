using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CriminalIntent.Model
{
    class CrimeLab
    {
        static CrimeLab crimeLab;

        CrimeLab(Context context) { }

        public List<Crime> Crimes { get; } = Enumerable
            .Range(0, 100)
            .Select(i => new Crime()
            {
                Title = $"Crime #{i}",
                IsSolved = i % 2 == 0
            })
            .ToList();

        public static CrimeLab Instance(Context context)
        {
            if (crimeLab == null)
                crimeLab = new CrimeLab(context);
            return crimeLab;
        }

        public Crime this[Guid id] => Crimes.Where(c => c.Id == id).SingleOrDefault();
    }
}