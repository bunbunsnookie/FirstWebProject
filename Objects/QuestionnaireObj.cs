using Web_2k.Enums;

namespace WebApplication1.Objects
{
    public class QuestionnaireObj
    {
        public string email { get; set; } 

        public List<string> know { get; set; } 

        public List<string> wantKnow { get; set; } 

        public bool searchCompanion { get; set; }

        public bool wantBeMentor { get; set; } 

        public bool searchMentor { get; set; }

        public string aboutMe { get; set; }

    }
}
