namespace ODataWebAPI.Models
{
    public sealed class User
    {
               public User()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName => string.Join(" ", FirstName, LastName);
        public Address Address { get; set; } = default!;
        public UserTypeEnum UserType { get; set; } = UserTypeEnum.Admin;
        public string UsertTypeName => UserType.Name;
        public int UserTypeValue => UserType.Value;


    }
}
