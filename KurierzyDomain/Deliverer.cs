using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KurierzyDomain
{
    public class Deliverer
    {
        public int PersonId { get; set; }
        [ForeignKey ("PersonId")]
        public virtual Person Person { get; set; }
        public DateTime Working_Since { get; set; }
        public List<Package> Packages { get; set; }
        public List<Van> Vans { get; set; }
    }
}
