namespace KurierzyDomain
{
    public class Office
    {
        public int Id { get; set; }
        public string Address { get; set; } 
        public List<OfficeWorker> Workers { get; set; }
    }
}
