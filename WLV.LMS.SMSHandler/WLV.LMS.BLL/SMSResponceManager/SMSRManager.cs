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
using System.Threading.Tasks;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BLL.APIService;
using WLV.LMS.BLL.DBService;
using WLV.LMS.BO.BussinessObject;
using WLV.LMS.BO.SMS;
using System.Threading;

namespace WLV.LMS.BLL.SMSResponceManager
{
    public class SMSRManager : ISMSRManager
    {
        private readonly BookService _bookService;
        private readonly MemberDBService _memberDBService;
        public SMSRManager()
        {
            _bookService = new BookService();
            _memberDBService = new MemberDBService();
        }

        public async Task<string> GetData(string MobileNumber,string Message)
        {
            string Msg = "";
            string MemberName = _memberDBService.ValidateMember(MobileNumber);
            if (string.IsNullOrEmpty(MemberName))
            {
                MemberDTO member= await _bookService.ValdiateMember(MobileNumber);
                if (member!=null)
                {
                    MemberName = member.FirstName + " " + member.LastName;
                //    _memberDBService.Insert(member);
                }
                else
                {
                    return "Mobile number is not recognize please Contact Admin";
                }
            }
            
            if (!String.IsNullOrEmpty(Message))
            {
                if (Message.ToLower().Contains("info"))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(Info.info.Replace("{Name}", MemberName));
                    sb.AppendLine(Info.ISBNSearch);
                    sb.AppendLine(Info.ISBNReserve + Info.Reserve);
                    return sb.ToString();
                }
                else if (Message.ToLower().Contains("search"))
                {
                    string ISBN = Message.Split('#')[1];
                    Book book = await _bookService.SearchBook(ISBN);
                    StringBuilder sb = new StringBuilder();
                    if(book == null)
                    {
                        sb.AppendLine("Book Not Found with Isbn "+ ISBN);
                    }
                    else
                    {
                        sb.AppendLine("Title - "+book.Title);
                        sb.AppendLine("Publisher - " + book.Publisher );
                        sb.AppendLine("PublishedDate - " + book.PublishedDate );                        
                        sb.AppendLine("Availablity - "+(book.BookCurrentCount == 0?"Not Available Now":"Available"));
                    }                  
                    return sb.ToString();
                }
                else if (Message.ToLower().Contains("reserve"))
                {
                    StringBuilder sb = new StringBuilder();
                    try
                    {
                        string[] Data = Message.Split(',').ToArray();
                        if (Data.Count() == 2)
                        {
                            ReserveBookDTO reserveBookDTO = new ReserveBookDTO();
                            reserveBookDTO.ISBN = Data[0].Split('#')[1];


                            string dt = Data[1].Split('#')[1];
                            reserveBookDTO.BorrowDate =dt;
                            reserveBookDTO.MobileNumber = MobileNumber;
                            string Responce = await _bookService.ReserveBook(reserveBookDTO);
                            if (!String.IsNullOrEmpty( Responce))
                            {
                                Msg = Responce;
                            }
                            else
                            {
                                Msg = "Reservation Failed";
                            }
                        }
                        else
                        {
                            Msg = "Invalid Format";
                        }

                    }
                    catch(Exception ex)
                    {
                        return ex.Message;
                    }             
                }
                else 
                {
                    Msg = "Invalid Format";
                }
            }
            else
            {
                Msg = "Invalid Format";
            }
            return Msg;
        } 
    }
}