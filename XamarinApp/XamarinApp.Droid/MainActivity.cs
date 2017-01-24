using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Gcm.Client;
using Android.Util;

namespace XamarinApp.Droid
{
    public static class Consts
    {
        public static string[] SenderIds { get; } = new[] { "送信者ID" };
    }

    [Activity(Label = "XamarinApp.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            try
            {
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                GcmClient.Register(this, Consts.SenderIds);
            }
            catch (Java.Net.MalformedURLException)
            {
                Log.Debug(nameof(MainActivity), "Error");
            }
            catch (Exception ex)
            {
                Log.Debug(nameof(MainActivity), ex.ToString());
            }
        }


    }
}

