using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Web_2k.Enums;
using Web_2k.Models;
using Web_2k.Objects;
using WebApplication1.Objects;
using dbModels = Web_2k.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.Extensions.Options;


namespace Web_2k.Controllers
{
    public class QuestionnaireController : Controller
    {

        [HttpGet]
        [Route("api/questionnaire")]
        public async Task<object> GetQuestionnaire()
        {


            string currentUserId = this.User.FindFirstValue(JwtRegisteredClaimNames.Jti);
            
            SqlConnection conn = new SqlConnection("Server = (localdb)\\mssqllocaldb; Database = applicationdb; Trusted_Connection = True;");
            conn.Open();
            
            string Questionnaire = "SELECT * FROM [Questionnaires] JOIN [Users] ON Questionnaires.UserId = Users.Id WHERE UserId='" + currentUserId + "'";
            SqlCommand cmd = new SqlCommand(Questionnaire, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    QuestionnaireObj obj = new QuestionnaireObj();
                    obj.email = reader.GetValue(1).ToString();
                    obj.know = JsonSerializer.Deserialize<List<string>>(reader.GetValue(2).ToString());
                    obj.wantKnow = JsonSerializer.Deserialize<List<string>>(reader.GetValue(3).ToString());
                    obj.searchCompanion = (bool)reader.GetValue(4);
                    obj.wantBeMentor = (bool)reader.GetValue(5);
                    obj.searchMentor = (bool)reader.GetValue(6);
                    obj.aboutMe = reader.GetValue(7).ToString();
                    conn.Close();
                    return obj;
                }
            }
            conn.Close();
            return false;

        }

        [HttpPost]
        [Route("api/save_questionnaire")]
        public async Task<Boolean> PostQuestionnaire([FromBody] QuestionnaireObj questionnaire)
        {


            string currentUserId = this.User.FindFirstValue(JwtRegisteredClaimNames.Jti);
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };

            SqlConnection conn = new SqlConnection("Server = (localdb)\\mssqllocaldb; Database = applicationdb; Trusted_Connection = True;");
            conn.Open();

            string check = "SELECT count(*) FROM [Questionnaires] WHERE UserId='" + currentUserId + "'";
            SqlCommand cmd = new SqlCommand(check, conn);
            bool exists = (int)cmd.ExecuteScalar() > 0;
            if (exists)
            {
                string update = "UPDATE [Questionnaires] SET Mail = '"
                + questionnaire.email + "', KnowInterests = '" + JsonSerializer.Serialize(questionnaire.know, options) +
                "', WantKnowInterests = '" + JsonSerializer.Serialize(questionnaire.wantKnow, options) +
                "', InSearchСompanion = '" + questionnaire.searchCompanion + "', WantBeMentor = '" + questionnaire.wantBeMentor + "', InSearchMentor = '"
                + questionnaire.searchMentor + "', AboutMe ='" + questionnaire.aboutMe + "' WHERE UserId= '" + currentUserId + "'";
                SqlCommand cmd1 = new SqlCommand(update, conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
                return true;
            }

            string new_questionnaire = "INSERT INTO [Questionnaires] (Id, Mail, KnowInterests, WantKnowInterests, InSearchСompanion," +
                " WantBeMentor, InSearchMentor, AboutMe, UserId) VALUES ("
                + "'" + Guid.NewGuid().ToString() + "', '" + questionnaire.email + "', '" + JsonSerializer.Serialize(questionnaire.know, options) + 
                "', '" + JsonSerializer.Serialize(questionnaire.wantKnow, options) +
                "', '" + questionnaire.searchCompanion + "', '" + questionnaire.wantBeMentor + "', '"
                + questionnaire.searchMentor + "', '" + questionnaire.aboutMe + "', '" + currentUserId + "')"; 

            SqlCommand cmd2 = new SqlCommand(new_questionnaire, conn);
            cmd2.ExecuteNonQuery();
            conn.Close();
            return true;

        }

    }
}
