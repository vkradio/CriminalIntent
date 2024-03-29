﻿using System;
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
    class Crime
    {
        public Crime()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }

        public Guid Id { get; }

        public string Title { get; set; }

        public DateTime Date { get; set; }

        public bool IsSolved { get; set; }
    }
}