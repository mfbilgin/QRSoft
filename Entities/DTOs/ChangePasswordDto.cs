using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class ChangePasswordDto : IDto
    {
        public int CompanyId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}