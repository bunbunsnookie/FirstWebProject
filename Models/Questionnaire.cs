using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Web_2k.Models
{
    public class Questionnaire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.Empty;

        public string Mail { get; set; } = string.Empty;

        List<String> KnowInterests { get; set; } = new List<String>();

        List<String> WantKnowInterests { get; set; } = new List<String>();

        public bool InSearchСompanion { get; set; } = false;

        public bool WantBeMentor { get; set; } = false;

        public bool InSearchMentor { get; set; } = false;

        public string AboutMe { get; set; } = string.Empty;

        [Display(Name = "User")]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
