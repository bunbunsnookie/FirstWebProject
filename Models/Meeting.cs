using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_2k.Models
{
    public class Meeting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.Empty; 

        public string Data { get; set; } = string.Empty;

        public string FormCommunication { get; set; } = string.Empty;

        public string LinkToCompanion { get; set; } = string.Empty;

        public string MailCompanion { get; set; } = string.Empty;

        public string Review { get; set; } = string.Empty;
    }
}
