using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class CartEntryViewModel
    {
        public Disk Disk { get; }
        [Display(Name = "Количество")]
        public int Count { get; }
        [Display(Name = "Всего")]
        public decimal Total { get; }

        public CartEntryViewModel(Disk disk, CartEntry cartEntry)
        {
            Disk = disk;
            Count = cartEntry.Count;
            Total = Disk.Price * Count;
        }
    }
}
