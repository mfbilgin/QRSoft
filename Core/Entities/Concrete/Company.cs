using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{

    public class Company : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string MailAddress { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }

    }
}