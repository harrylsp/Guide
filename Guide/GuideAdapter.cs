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
using Java.Lang;

namespace Guide
{
    public class GuideAdapter : Android.Support.V4.View.PagerAdapter
    {
        private List<View> list;

        public GuideAdapter(List<View> list)
        {
            this.list = list;
        }

        public override int Count
        {
            get { return list.Count; }

        }

        public override bool IsViewFromObject(View arg0, Java.Lang.Object arg1)
        {
            return arg0 == arg1;
        }

        public override void DestroyItem(ViewGroup view, int position, Java.Lang.Object obj)
        {
            view.RemoveView(list[position]);
        }

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            container.AddView(list[position]);
            return list[position];
        }

    }
}