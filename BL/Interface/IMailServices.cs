namespace MailingUsingMailkit.BL.Interface
{
    public interface IMailServices
    {
        public  Task SendAsync(string to, string subject, string body, List<IFormFile> attachments = null);
    }
}
