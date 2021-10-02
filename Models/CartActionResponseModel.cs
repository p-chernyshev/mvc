using System.Linq;

namespace Mvc.Models
{
    public class CartActionResponseModel
    {
        public CartEntryViewModel DiskEntry { get; }
        public int Length { get; }
        public decimal Total { get; }

        public CartActionResponseModel(CartViewModel cart, int diskId)
        {
            DiskEntry = cart.Entries.FirstOrDefault(entry => entry.Disk.Id == diskId);
            Length = cart.Entries.Select(entry => entry.Count).Sum();
            Total = cart.Total;
        }
    }
}
