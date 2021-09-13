namespace Mvc.Models
{
    public class Disk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SizeGb { get; set; }
        public int WriteSpeedMbps { get; set; }
        public int ReadSpeedMbps { get; set; }
        public decimal Price { get; set; }
        public DiskType Type { get; set; }
        public DiskForm Form { get; set; }
    }

    public enum DiskType
    {
        SSD,
        HDD,
    }

    public enum DiskForm
    {
        Sata25,
        Sata35,
        M2,
    }
}
