using Core.Entities.Abstract;

namespace Entities.DTOs
{
    //Şirketleri kodlarıyla birlikte döndürmek için kullanılan DTO(data transfer object)dur.
    public class CompanyWithCodeDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}