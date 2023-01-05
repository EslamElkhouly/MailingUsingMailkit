using MailingUsingMailkit.BL.Interface;
using MailingUsingMailkit.BL.MapAppsettings;
using Microsoft.Extensions.Options;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace SendMailUsingMailkit.BL.Repository
{
    public class MailServices : IMailServices
    {
        private readonly MailSettings _mail;

        public MailServices(IOptions<MailSettings> mail)
        {
            _mail = mail.Value;
        }
        public async Task SendAsync(string to, string subject, string body, List<IFormFile> attachments = null)
        {
            var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mail.Email),
                Subject= subject,
                               
            };
            email.To.Add(MailboxAddress.Parse(to));
            var builder = new BodyBuilder();
            
            if ((attachments !=null) && (attachments.Count>0) )
            {
                byte[] fileBytes;
                foreach (var attachment in attachments)
                {
                    using (var ms=new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes=ms.ToArray();
                    }
                    builder.Attachments.Add(attachment.FileName, fileBytes);
                }
            }
            
            builder.HtmlBody = body;

            email.Body = builder.ToMessageBody();

            email.From.Add(new MailboxAddress(_mail.DisplayName,_mail.Email));

            using (var smtp=new SmtpClient())
            {
                smtp.Connect(_mail.Host,_mail.Port,false);
                smtp.Authenticate(_mail.Email,_mail.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

        }
   
    
    
    }


    
}
