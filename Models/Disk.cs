using System.ComponentModel.DataAnnotations;

namespace Mvc.Models
{
    public class Disk
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Размер")]
        public int SizeGb { get; set; }
        [Display(Name = "Скорость записи")]
        public int WriteSpeedMbps { get; set; }
        [Display(Name = "Скорость чтения")]
        public int ReadSpeedMbps { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Display(Name = "Тип")]
        public DiskType Type { get; set; }
        [Display(Name = "Формат")]
        public DiskForm Form { get; set; }
    }

    public enum DiskType
    {
        SSD,
        HDD,
    }

    public enum DiskForm
    {
        [Display(Name = "SATA 2.5\"")]
        Sata25,
        [Display(Name = "SATA 3.5\"")]
        Sata35,
        [Display(Name = "M.2")]
        M2,
    }
}
