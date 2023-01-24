using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web_2k.Enums;
using Web_2k.Objects;

namespace Web_2k.Controllers
{
    public class AuthController : Controller
    {

        [HttpPost]
        [Route("auth/login")]
        public async Task<AuthInfo> Login([FromBody] AuthInput input)
        {

            if (input.Login == "test" && input.Password == "admin")
            {
                await SetCookies(input.Login, RoleType.Admin, "test");
                return AuthInfo.Success(RoleType.Admin, input.Login);
            }

            SqlConnection conn = new SqlConnection("Server = (localdb)\\mssqllocaldb; Database = applicationdb; Trusted_Connection = True;");
            conn.Open();
            string checkUser = "SELECT TOP 1 Id FROM [Users] WHERE Login='" + input.Login + "'" + " AND " + "Password = '" + input.Password + "'";
            SqlCommand cmd = new SqlCommand(checkUser, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            string i = null;
            while (reader.Read())
            {
                i = reader.GetValue(0).ToString();
                break;
                
            }
            if(i != null)
            {
                await SetCookies(input.Login, RoleType.User, i);
                return AuthInfo.Success(RoleType.User, input.Login);
            }
            conn.Close();

            return AuthInfo.Fail();
        }

        [HttpGet]
        [Route("auth/logout")]
        public async Task<AuthInfo> Logout()
        {
            await RemoveCookies();
            return AuthInfo.Fail();
        }

        [HttpGet]
        [Route("auth/check")]
        public AuthInfo GetInfo()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                string role = User.FindFirstValue(ClaimsIdentity.DefaultRoleClaimType);
                RoleType roleType = Role.FromString(role);
                return AuthInfo.Success(roleType, User.Identity.Name);
            }

            return AuthInfo.Fail();
        }


        private async Task SetCookies(string login, RoleType role, String guid)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, guid),
                };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await AuthenticationHttpContextExtensions.SignInAsync(HttpContext, new ClaimsPrincipal(id));
        }

        private async Task RemoveCookies()
        {
            await AuthenticationHttpContextExtensions.SignOutAsync(HttpContext);
        }
    }
}
