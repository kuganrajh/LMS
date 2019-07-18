using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BO.Book;
using WLV.LMS.BO.Enums;
using WLV.LMS.BO.SystemData;
using WLV.LMS.Common.Logging;
using WLV.LMS.Common.MailService;
using WLV.LMS.DAL.Interfaces;
using WLV.LMS.DAL.Repositories;

namespace WLV.LMS.BLL.Services
{
    public class ReservationExpireService : IReservationExpireService
    {
        private readonly IReservationExpireRepository _repository;
        private readonly IRepository<SystemOption> _repositorySystemOption;

        public ReservationExpireService()
        {
            _repository = new ReservationExpireRepository();
            _repositorySystemOption = new SystemOptionRepository();
        }

        public async void ExpireReservation()
        {
            try
            {
                List<ReserveBook> ReserveBooksTOUpdate = new List<ReserveBook>();
                var SystemData = _repositorySystemOption.Get();
                string ReservationExpireDay = SystemData.Where(s => s.Name == LibraryOption.ReservationExpireDay.ToString()).FirstOrDefault().Value;
                List<ReserveBook> ReserveBooksTOCheck = _repository.Get();
                foreach (ReserveBook ReserveBookTOCheck in ReserveBooksTOCheck)
                {
                    if (ReserveBookTOCheck.BorrowBook.Count == 0 && ValidateToExpire(ReserveBookTOCheck.BarrowDate, Convert.ToInt32(ReservationExpireDay)))
                    {
                        ReserveBookTOCheck.UpdatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        ReserveBookTOCheck.IsActive = false;
                        ReserveBooksTOUpdate.Add(ReserveBookTOCheck);
                    }
                }
                if (ReserveBooksTOUpdate.Count > 0)
                {
                    List<ReserveBook> resuklt = await _repository.UpdateAsync(ReserveBooksTOUpdate);
                }
                foreach (var item in ReserveBooksTOUpdate)
                {
                    PrepareMail(SystemData, item);
                }

               // PrepareMail(SystemData, ReserveBooksTOCheck.FirstOrDefault());
            }
            catch (Exception ex)
            {

            }

        }

        private void PrepareMail(List<SystemOption> SystemData, ReserveBook reserveBook)
        {
            try
            {  
                string body = string.Empty;
                body+= "<h5> Hi " + reserveBook.Member.FirstName + " " + reserveBook.Member.LastName+ "</h5>";
                body += "<p>Your Reservation Id - <b style = \"color: red\">RSV" + reserveBook.Id + "</b> is Expired</p>";
                body += " </br><p>Thanks & Best Regards,</p></br><p>LMS Admin.</p>";
                EmailMessage message = new EmailMessage();
                message.Subject = "Reservation Expired";
                message.To = reserveBook.Member.Email;
                message.MessageBody = body;
                OutgoingMailSetting outgoingMailSetting = new OutgoingMailSetting();
                outgoingMailSetting.DisplayName = "kuganrajh";
                outgoingMailSetting.MailAddress = SystemData.Where(s => s.Name == MailDetail.FromEmailId.ToString()).FirstOrDefault().Value;
                outgoingMailSetting.Password = SystemData.Where(s => s.Name == MailDetail.FromEmailPassword.ToString()).FirstOrDefault().Value;
                outgoingMailSetting.SmtpServer = SystemData.Where(s => s.Name == MailDetail.SmtpClient.ToString()).FirstOrDefault().Value;
                outgoingMailSetting.SmtpPort = Convert.ToInt32(SystemData.Where(s => s.Name == MailDetail.PortId.ToString()).FirstOrDefault().Value);
                EmailManager.SendEmail(message, outgoingMailSetting);
            }
            catch (Exception ex)
            {
                LogManager.Log(LogSeverity.Error, LogModule.Web, ex.Message, ex);
            }
        }
        private bool ValidateToExpire(DateTime ReservedDateTime, int AddDays)
        {
            var dtNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

            var dtAddDays = ReservedDateTime.AddDays(AddDays);

            if (dtAddDays < dtNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
