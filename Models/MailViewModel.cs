using System.ComponentModel.DataAnnotations;

namespace MailingUsingMailkit.Models
{
    public class MailViewModel
    {
        [Required (ErrorMessage ="Please Enter the Receiver Email")]
        public string To { get; set; }
        public string Body { get; set; }
        public string subject { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
