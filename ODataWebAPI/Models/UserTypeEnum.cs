using Ardalis.SmartEnum;

namespace ODataWebAPI.Models
{
    public sealed class UserTypeEnum : SmartEnum<UserTypeEnum>
    {
        public static UserTypeEnum Admin = new("Admin", 1);
        public static  UserTypeEnum Customer = new("User", 2);
        private UserTypeEnum(string name, int value) : base(name, value)
        {
        }
    }
}
