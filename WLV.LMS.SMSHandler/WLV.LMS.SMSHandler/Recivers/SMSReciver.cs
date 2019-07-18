
using Android.App;
using Android.Content;
using Android.Widget;
using Android.Telephony;
using Android.Provider;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BLL.SMSResponceManager;
using System.Collections.Generic;
using System.Linq;

namespace WLV.LMS.SMSHandler.Recivers
{
    [BroadcastReceiver(Enabled = true, Exported = true)]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" },Priority =(int) IntentFilterPriority.HighPriority)]
    public class SMSReciver : BroadcastReceiver
    {
        private readonly ISMSRManager _sMSRManager;
       
        public SMSReciver()
        {
            _sMSRManager = new SMSRManager();
        }
        private const string TAG = "AA:SmsReceiver";
        public override async void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(Telephony.Sms.Intents.SmsReceivedAction))
            {
                var msgs = Telephony.Sms.Intents.GetMessagesFromIntent(intent);
                foreach (var msg in msgs)
                {
                    string result = await _sMSRManager.GetData(msg.OriginatingAddress, msg.MessageBody);
                    SmsManager sm = SmsManager.Default;
                    if (result.Length >= 150)
                    {
                        List<string> parts = new List<string>();
                        //split the string into chunks of 20 chars.
                        var enumerable = Enumerable.Range(0, result.Length / 149).Select(i => result.Substring(i * 149, 149));
                        parts = enumerable.ToList();
                        int banace = result.Length % 149;
                        parts.Add(result.Substring(parts.Count * 149, banace));
                        sm.SendMultipartTextMessage(msg.OriginatingAddress, null, parts, null, null);
                    }
                    else
                    {
                        sm.SendTextMessage(msg.OriginatingAddress, null, result, null, null);
                    }
                    //SmsManager.Default.SendTextMessage(msg.OriginatingAddress, null, result, null, null);

                }
            }

            Toast.MakeText(context, "Received intent!", ToastLength.Short).Show();

        }
    }
}