namespace KurierzyDomain
{
    public class Van
    {
        public int Id { get; set; } 
        public string Registration_Plate { get; set; }  
        public Deliverer[] Drivers { get; set; }    
        
    }
}
