using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Gcm.Client;

[assembly: Permission(Name = "com.chorus_st.xamarinapp.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.chorus_st.xamarinapp.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]
//GET_ACCOUNTS is only needed for android versions 4.0.3 and below
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
namespace XamarinApp.Droid
{
    [BroadcastReceiver(Permission = Gcm.Client.Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { "com.chorus_st.xamarinapp" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { "com.chorus_st.xamarinapp" })]
    [IntentFilter(new string[] { Gcm.Client.Constants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { "com.chorus_st.xamarinapp" })]
    public class PushHandlerBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
    }

    [Service]
    public class GcmService : GcmServiceBase
    {
        public GcmService() : base(Consts.SenderIds)
        {
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            var message = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(message))
            {
                this.CreateNotification("New todo item!", $"Todo item: {message}");
                return;
            }
        }
        private void CreateNotification(string title, string description)
        {
            var notificationManager = this.GetSystemService(Context.NotificationService) as NotificationManager;
            var uiIntent = new Intent(this, typeof(MainActivity));

            var builder = new NotificationCompat.Builder(this);
            var notification = builder.SetContentIntent(PendingIntent.GetActivity(this, 0, uiIntent, 0))
                .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                .SetTicker(title)
                .SetContentTitle(title)
                .SetContentText(description)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .SetAutoCancel(true)
                .Build();

            notificationManager.Notify(1, notification);
        }

        protected override void OnError(Context context, string errorId)
        {
            //throw new NotImplementedException();
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            //throw new NotImplementedException();
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            //throw new NotImplementedException();
        }
    }
}