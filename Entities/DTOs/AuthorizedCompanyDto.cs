using Core.Entities.Abstract;
using Core.Utilities.Security.Jwt;

namespace Entities.DTOs
{
    public class AuthorizedCompanyDto : IDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}