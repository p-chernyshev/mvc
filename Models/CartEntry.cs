namespace Mvc.Models
{
    public class CartEntry
    {
        public int DiskId { get; set; }
        public int Count { get; set; } = 1;

        public CartEntry(int diskId)
        {
            DiskId = diskId;
        }
    }
}
