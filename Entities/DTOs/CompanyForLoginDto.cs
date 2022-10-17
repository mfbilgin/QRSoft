using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class CompanyForLoginDto : IDto
    {
        public string MailAddress { get; set; }
        public string  Password { get; set; }
    }
}