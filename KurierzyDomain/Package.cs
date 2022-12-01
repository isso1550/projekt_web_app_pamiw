using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace KurierzyDomain
{
    public class Package
    {
        public int Id { get; set; }
        public int Weight { get; set; }      
        public string Delivery_Address { get; set; }
        public DateTime Send_Date { get; set; }
        public DateTime? Deliver_Date { get; set; }
        public int DelivererId { get; set; }
        public virtual Deliverer Deliverer { get; set; }
        public override string ToString()
        {
            return ("Package " + Id);
        }
    }

    
}
