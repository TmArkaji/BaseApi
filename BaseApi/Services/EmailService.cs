using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BaseApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private const string password = "lhcb oliv kpvw gqbh";

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
            smtpSettings.Password = password;

            using (var client = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                Credentials = new NetworkCredential(smtpSettings.UserName, smtpSettings.Password),
                EnableSsl = smtpSettings.EnableSsl,
            })
            using (var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpSettings.From),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                mailMessage.To.Add(smtpSettings.To);

                await client.SendMailAsync(mailMessage);
            }
        }
    }
}




public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSsl { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string From { get; set; }
    public string To { get; set; }
}
