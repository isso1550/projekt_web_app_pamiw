namespace KurierzyDomain
{
    public class Deliverer
    {
        public virtual Person Person { get; set; }
        public DateTime Working_Since { get; set; }
        public Package[] Packages_Delivered { get; set; }
        public Van[] Assigned_Vans { get; set; }
    }
}
