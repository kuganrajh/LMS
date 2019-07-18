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
using Android.Database.Sqlite;
using WLV.LMS.DAL.Repository;

namespace WLV.LMS.DAL.Infrastructure
{
    public class DataStore : SQLiteOpenHelper
    {
        private const string _DatabaseName = "LMS.db";

        public DataStore(Context context) : base(context, _DatabaseName, null, 1)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(MemberRepository.CreateQuery);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(MemberRepository.DeleteQuery);            
            OnCreate(db);
        }
    }
}