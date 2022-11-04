namespace KurierzyDomain
{
    public class OfficeWorker
    {
        public virtual Person Person { get; set; }
        public virtual Office Office { get; set; }
    }
}
