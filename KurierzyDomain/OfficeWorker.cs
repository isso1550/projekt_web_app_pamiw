using System.ComponentModel.DataAnnotations.Schema;

namespace KurierzyDomain
{
    public class OfficeWorker
    {
        public int PersonId { get; set; }
        [ForeignKey ("PersonId")]
        public virtual Person Person { get; set; }
        public int OfficeId { get; set; }
        public  Office Office { get; set; }
        public string passwordHash { get; set; }
    }
}
