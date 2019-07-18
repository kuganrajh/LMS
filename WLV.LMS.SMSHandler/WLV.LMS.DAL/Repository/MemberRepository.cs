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
using WLV.LMS.DAL.Interfaces;
using Android.Database.Sqlite;
using WLV.LMS.BO.SystemConfigData;
using WLV.LMS.DAL.Infrastructure;
using Android.Database;
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.DAL.Repository
{

    public class MemberRepository : IMemberRepository
    {

        private const string TableName = "Member";
        private const string ColumnID = "Id";
        private const string ColumnRefNumber = "RefNumber";
        private const string ColumnFirstName = "FirstName";
        private const string ColumnLastName = "LastName";
        private const string ColumnSSID = "SSID";
        private const string ColumnMobileNumber = "MobileNumber";


        public const string CreateQuery = "CREATE TABLE " + TableName + " ( "
            + ColumnID + " INTEGER PRIMARY KEY,"
            + ColumnRefNumber + " TEXT,"
            + ColumnFirstName + " TEXT,"
            + ColumnLastName + " TEXT,"
            + ColumnSSID + " TEXT,"
            + ColumnMobileNumber + " TEXT)";


        public const string DeleteQuery = "DROP TABLE IF EXISTS " + TableName;

        private readonly SQLiteDatabase _db;
        public MemberRepository()
        {
            _db = new DataStore(ApplicationActivityContext.context).WritableDatabase;
        }


        public bool Insert(MemberDTO MemberDTO)
        {
            ContentValues contentValues = new ContentValues();
            contentValues.Put(ColumnRefNumber, MemberDTO.RefNumber);
            contentValues.Put(ColumnFirstName, MemberDTO.FirstName);
            contentValues.Put(ColumnLastName, MemberDTO.LastName);
            contentValues.Put(ColumnSSID, MemberDTO.SSID);
            contentValues.Put(ColumnMobileNumber, MemberDTO.MobileNumber);
            long flag = _db.Insert(TableName, null, contentValues);
            return flag > 0;
        }

        public string ValidateMember(string MobileNumber)
        {
            string Name = String.Empty;
            string[] columns = new string[] { ColumnID, ColumnMobileNumber, ColumnFirstName, ColumnLastName };

            using (ICursor cursor = _db.Query(TableName, columns, null, null, null, null, null))
            {
                while (cursor.MoveToNext())
                {
                    try
                    {
                        if (cursor.GetString(cursor.GetColumnIndexOrThrow(ColumnMobileNumber)).Contains(MobileNumber))
                        {
                            Name = cursor.GetString(cursor.GetColumnIndexOrThrow(ColumnFirstName)) + " "+ cursor.GetString(cursor.GetColumnIndexOrThrow(ColumnLastName));
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            return Name;
        }
    }
}