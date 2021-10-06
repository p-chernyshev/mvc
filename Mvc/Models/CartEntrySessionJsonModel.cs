namespace Mvc.Models
{
    public class CartEntrySessionJsonModel
    {
        public int DiskId { get; set; }
        public int Count { get; set; } = 1;

        public CartEntrySessionJsonModel(int diskId)
        {
            DiskId = diskId;
        }
    }
}
