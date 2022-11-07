using Core.Entities.Abstract;

namespace Entities.Concrete
{
    //Şirketleri birbirinden ayırmak için kullanılan ve url kısmında belirleyici faktör olması için Şirket Kodları.
    public class CompanyCode : IEntity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Code { get; set; }
    }
}