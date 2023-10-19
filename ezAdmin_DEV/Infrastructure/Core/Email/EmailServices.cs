using Infrastructure.ConstantsDefine.AppSetting;
using Models.Models.ParamsFunction;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;

namespace Infrastructure.Core.Email
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramsSendEmail"></param>
        /// <returns></returns>
        public async Task<Response> SendEmailAsync(ParamsSendGridEmail paramsSendEmail)
        {
            string? fromEmail = _configuration[EmailKeys.SENDER_EMAIL];
            string? fromName = _configuration[EmailKeys.SENDER_NAME];
            string? apiKey = _configuration[EmailKeys.API_KEY];
            var sendGridClient = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail, fromName);
            var to = new EmailAddress(paramsSendEmail.ToEmailAddress);
            var plainTextContent = Regex.Replace(paramsSendEmail.HtmlMessage, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, paramsSendEmail.Subject, plainTextContent, paramsSendEmail.HtmlMessage);
            return await sendGridClient.SendEmailAsync(msg);
        }
    }
}
