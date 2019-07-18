using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLV.LMS.BLL.Interfaces;
using WLV.LMS.BO.Book;
using WLV.LMS.BO.Enums;
using WLV.LMS.BO.Member;
using WLV.LMS.BO.SystemData;
using WLV.LMS.Common.Logging;
using WLV.LMS.Common.MailService;
using WLV.LMS.DAL.Interfaces;
using WLV.LMS.DAL.Repositories;
using WLV.LMS.DTO.InputDTO;

namespace WLV.LMS.BLL.Services
{
    public class ReserveBookService : IDualService<ReserveBook, ReserveBookDTO>
    {
        private readonly IRepository<ReserveBook> _repository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Member> _memberRepository;
        private readonly IRepository<SystemOption> _repositorySystemOption;
        public ReserveBookService()
        {
            _repository = new ReserveBookRepository();
            _bookRepository = new BookRepository();
            _memberRepository = new MemberRepository();
            _repositorySystemOption = new SystemOptionRepository();
        }

        public List<ReserveBook> Get(string UserId = "")
        {
            //try
            //{
            //    var SystemData = _repositorySystemOption.Get();
            //    EmailMessage message = new EmailMessage();
            //    message.Subject = "Test sub";
            //    message.To = "kuganrajh@gmail.com";
            //    message.MessageBody = "test email";
            //    OutgoingMailSetting outgoingMailSetting = new OutgoingMailSetting();
            //    outgoingMailSetting.DisplayName = "kuganrajh";
            //    outgoingMailSetting.MailAddress = SystemData.Where(s => s.Name == MailDetail.FromEmailId.ToString()).FirstOrDefault().Value;
            //    outgoingMailSetting.Password = SystemData.Where(s => s.Name == MailDetail.FromEmailPassword.ToString()).FirstOrDefault().Value;
            //    outgoingMailSetting.SmtpServer = SystemData.Where(s => s.Name == MailDetail.SmtpClient.ToString()).FirstOrDefault().Value;
            //    outgoingMailSetting.SmtpPort = Convert.ToInt32( SystemData.Where(s => s.Name == MailDetail.PortId.ToString()).FirstOrDefault().Value);

            //    EmailManager.SendEmail(message, outgoingMailSetting);
            //}
            //catch (Exception ex)
            //{
            //    LogManager.Log(LogSeverity.Error, LogModule.Web, ex.Message, ex);
            //}
            return _repository.Get(UserId);
        }

        public async Task<ReserveBook> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task<ReserveBook> CreateAsync(ReserveBookDTO reserveBookDTO)
        {
            var data = _memberRepository.Get().Where(m => m.MobileNumber.Contains(reserveBookDTO.MobileNumber) && m.IsActive).FirstOrDefault();
            if (data != null)
            {
                int MemberID = data.Id;
                ReserveBook model = new ReserveBook();
                model.BarrowDate = Convert.ToDateTime(reserveBookDTO.BorrowDate);
                model.BookId = _bookRepository.Get().Where(b => b.ISBN == reserveBookDTO.ISBN).FirstOrDefault().Id;
                model.MemberId = MemberID;
                return await _repository.CreateAsync(model);
            }
            else
            {
                return null;
            }
            
        }

        public async Task<ReserveBook> UpdateAsync(ReserveBook model)
        {
            return await _repository.UpdateAsync(model);
        }

        public int DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
