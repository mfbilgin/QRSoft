using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class CompanyForRegisterDto : IDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public string Password { get; set; }
    }
}