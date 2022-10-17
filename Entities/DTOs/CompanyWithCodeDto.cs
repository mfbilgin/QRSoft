using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class CompanyWithCodeDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}