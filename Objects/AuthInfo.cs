using Web_2k.Enums;

namespace Web_2k.Objects
{
    public class AuthInfo
    {
        public bool CheckAutorization { get; set; }
        public RoleType Role { get; set; }
        
        public string Username{ get; set; }

        public static AuthInfo Success(RoleType role, string username)
        {
            return new AuthInfo { CheckAutorization = true, Role = role, Username = username};
        }
        public static AuthInfo Fail()
        {
            return new AuthInfo { CheckAutorization = false };
        }
    }
}
