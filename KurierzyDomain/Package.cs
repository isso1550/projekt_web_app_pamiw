namespace KurierzyDomain
{
    public class Package
    {
        public int Id { get; set; }
        public virtual Deliverer Deliverer { get; set; }
        public virtual Person Sender { get; set; }
        public virtual Person Receiver { get; set; }
        public int Size { get; set; }
    }
}
