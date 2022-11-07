using Core.Entities.Abstract;

namespace Entities.DTOs
{
    //Login için kullanıcıdan alınnan bilgileri içerir. Veri tabanına bağlı değildir.
    public class CompanyForLoginDto : IDto
    {
        public string MailAddress { get; set; }
        public string  Password { get; set; }
    }
}