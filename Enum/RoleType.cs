namespace Web_2k.Enums
{
    public enum RoleType
    {
        User = 0,
        Admin = 1,
        Undefined = 2,
    }

    public class Role
    {
        public static RoleType FromString(string str)
        {
            foreach (RoleType role in Enum.GetValues(typeof(RoleType)))
            {
                if (role.ToString() == str)
                    return role;
            }

            return RoleType.Undefined;
        }
    }
}
