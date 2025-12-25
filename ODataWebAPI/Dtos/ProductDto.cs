using ODataWebAPI.Models;

namespace ODataWebAPI.Dtos
{
    public sealed class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
    public sealed class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!; 
        public UserTypeEnum UserType { get; set; } = UserTypeEnum.Admin;
        public string UsertTypeName { get; set; } = default!;
        public int UserTypeValue { get; set; }
        public Address Address { get; set; } = default!;
    }
}
