using Core.Entities.Abstract;

namespace Entities.DTOs
{
    //Password değiştirmek için kullanıcıdan alınan bilgileri içerir. Veri tabanıyla bağlantılı değildir.
    public class ChangePasswordDto : IDto
    {
        public int CompanyId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}