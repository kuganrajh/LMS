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
using WLV.LMS.DAL.Repository;
using WLV.LMS.DAL.Interfaces;
using WLV.LMS.BO.BussinessObject;

namespace WLV.LMS.BLL.DBService
{
    public class MemberDBService
    {
        private readonly IMemberRepository _memberRepository;
        public MemberDBService()
        {
            _memberRepository = new MemberRepository();
        }
        public bool Insert(MemberDTO MemberDTO)
        {
            return _memberRepository.Insert(MemberDTO);
        }

        public string ValidateMember(string MobileNumber)
        {
            return _memberRepository.ValidateMember(MobileNumber);
        }
    }
}