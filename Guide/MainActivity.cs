using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using System.Collections.Generic;

namespace Guide
{
    [Activity(Label = "Guide", MainLauncher = true, Theme = "@android:style/Theme.Black.NoTitleBar", Icon = "@drawable/icon")]
    public class MainActivity : Activity, View.IOnClickListener, ViewPager.IOnPageChangeListener
    {
        #region 控件
        /// <summary>
        /// 底部小点的图片
        /// </summary>
        private LinearLayout linearLayout_Point;
        /// <summary>
        /// 
        /// </summary>
        private ViewPager viewpage;
        /// <summary>
        /// 立即进入按钮
        /// </summary>
        private TextView tv;
        #endregion

        #region 字段
        /// <summary>
        /// 引导图 图片集合
        /// </summary>
        private int[] imageView = {
            Resource.Drawable.GuideFirst, Resource.Drawable.GuideSecond
        };
        List<View> list;
        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            // Get our button from the layout resource,
            // and attach an event to it

            // 初始化控件
            linearLayout_Point = FindViewById<LinearLayout>(Resource.Id.point);
            viewpage = FindViewById<ViewPager>(Resource.Id.viewpage);
            tv = FindViewById<TextView>(Resource.Id.guideText);

            // 注册事件
            tv.SetOnClickListener(this);
            viewpage.AddOnPageChangeListener(this);

            // 给ViewPage 添加图片
            list = new List<View>();
            // 设置图片布局参数
            LinearLayout.LayoutParams ps = new LinearLayout.LayoutParams(ActionBar.LayoutParams.MatchParent, ActionBar.LayoutParams.MatchParent);
            // 创建引导图
            for (int i = 0; i < imageView.Length; i++)
            {
                ImageView iv = new ImageView(this);
                iv.LayoutParameters = ps;
                iv.SetScaleType(ImageView.ScaleType.FitXy);
                iv.SetImageResource(imageView[i]);
                list.Add(iv);
            }

            // 加入适配器
            viewpage.Adapter = new GuideAdapter(list);

            // 添加小圆点
            for (int i = 0; i < imageView.Length; i++)
            {
                // 圆点大小适配
                var size = Resources.GetDimensionPixelSize(Resource.Dimension.Size_18);
                LinearLayout.LayoutParams pointParams = new LinearLayout.LayoutParams(size, size);

                if (i < 1)
                {
                    pointParams.SetMargins(0, 0, 0, 0);
                }
                else
                {
                    pointParams.SetMargins(10, 0, 0, 0);
                }

                ImageView imageView = new ImageView(this);
                imageView.LayoutParameters = pointParams;

                imageView.SetBackgroundResource(Resource.Drawable.NoPress);
                linearLayout_Point.AddView(imageView);
            }

            // 设置默认选中第一张圆点
            linearLayout_Point.GetChildAt(0).SetBackgroundResource(Resource.Drawable.Press);
        }

        #region 点击事件
        public void OnClick(View v)
        {
            Toast.MakeText(this, "别瞎点", ToastLength.Short).Show();
        }
        #endregion

        #region ViewPage滑动事件
        public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
        {
        }

        public void OnPageScrollStateChanged(int state)
        {
        }
        /// <summary>
        /// 滑动到第几张图片
        /// </summary>
        /// <param name="position"></param>
        public void OnPageSelected(int position)
        {
            for (int i = 0; i < imageView.Length; i++)
            {
                if (i == position)
                {
                    linearLayout_Point.GetChildAt(position).SetBackgroundResource(Resource.Drawable.Press);
                }
                else
                {
                    linearLayout_Point.GetChildAt(i).SetBackgroundResource(Resource.Drawable.NoPress);
                }
            }

            // 滑动到最后一张图，显示按钮
            if (position == imageView.Length - 1)
            {
                tv.Visibility = ViewStates.Visible;
            }
            else
            {
                tv.Visibility = ViewStates.Gone;
            }
        }
        #endregion
    }
}

