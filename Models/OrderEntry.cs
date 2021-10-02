namespace Mvc.Models
{
    public class OrderEntry
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int DiskId { get; set; }
        public Disk Disk { get; set; }
        public int Count { get; set; }
    }
}
