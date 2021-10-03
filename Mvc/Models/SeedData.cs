using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mvc.Data;
using System;
using System.Linq;

namespace Mvc.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MvcDiskContext(serviceProvider.GetRequiredService<DbContextOptions<MvcDiskContext>>());

            SeedDisk(context);
            SeedCity(context);

            context.SaveChanges();
        }

        private static void SeedDisk(MvcDiskContext context)
        {
            if (context.Disk.Any()) return;

            context.Disk.AddRange(
                new Disk
                {
                    Name = "Transcend MTE110S 128GB",
                    SizeGb = 128,
                    WriteSpeedMbps = 400,
                    ReadSpeedMbps = 1600,
                    Price = 2050M,
                    Type = DiskType.SSD,
                    Form = DiskForm.M2,
                },
                new Disk
                {
                    Name = "HP EX900 120GB",
                    SizeGb = 120,
                    WriteSpeedMbps = 650,
                    ReadSpeedMbps = 1800,
                    Price = 2299M,
                    Type = DiskType.SSD,
                    Form = DiskForm.M2,
                },
                new Disk
                {
                    Name = "WD Blue SN550 250GB",
                    SizeGb = 250,
                    WriteSpeedMbps = 950,
                    ReadSpeedMbps = 2400,
                    Price = 3699M,
                    Type = DiskType.SSD,
                    Form = DiskForm.M2,
                },
                new Disk
                {
                    Name = "WD Black SN750 250GB",
                    SizeGb = 250,
                    WriteSpeedMbps = 1600,
                    ReadSpeedMbps = 3100,
                    Price = 4599M,
                    Type = DiskType.SSD,
                    Form = DiskForm.M2,
                },
                new Disk
                {
                    Name = "WD Blue SN550 500GB",
                    SizeGb = 500,
                    WriteSpeedMbps = 1750,
                    ReadSpeedMbps = 2400,
                    Price = 5299M,
                    Type = DiskType.SSD,
                    Form = DiskForm.M2,
                },
                new Disk
                {
                    Name = "Samsung 970 EVO 500GB",
                    SizeGb = 500,
                    WriteSpeedMbps = 3200,
                    ReadSpeedMbps = 3500,
                    Price = 7099M,
                    Type = DiskType.SSD,
                    Form = DiskForm.M2,
                },
                new Disk
                {
                    Name = "Apacer AS350 128GB",
                    SizeGb = 128,
                    WriteSpeedMbps = 540,
                    ReadSpeedMbps = 560,
                    Price = 1799M,
                    Type = DiskType.SSD,
                    Form = DiskForm.Sata25,
                },
                new Disk
                {
                    Name = "Apacer AS350 240GB",
                    SizeGb = 240,
                    WriteSpeedMbps = 520,
                    ReadSpeedMbps = 550,
                    Price = 2950M,
                    Type = DiskType.SSD,
                    Form = DiskForm.Sata25,
                },
                new Disk
                {
                    Name = "Apacer AS350 480GB",
                    SizeGb = 480,
                    WriteSpeedMbps = 510,
                    ReadSpeedMbps = 540,
                    Price = 4699M,
                    Type = DiskType.SSD,
                    Form = DiskForm.Sata25,
                },
                new Disk
                {
                    Name = "Seagate Barracuda 7200 1TB",
                    SizeGb = 1024,
                    WriteSpeedMbps = 210,
                    ReadSpeedMbps = 210,
                    Price = 2899M,
                    Type = DiskType.HDD,
                    Form = DiskForm.Sata35,
                },
                new Disk
                {
                    Name = "Seagate Barracuda 7200 2TB",
                    SizeGb = 2048,
                    WriteSpeedMbps = 220,
                    ReadSpeedMbps = 220,
                    Price = 4450M,
                    Type = DiskType.HDD,
                    Form = DiskForm.Sata35,
                },
                new Disk
                {
                    Name = "Seagate Barracuda 7200 3TB",
                    SizeGb = 3072,
                    WriteSpeedMbps = 210,
                    ReadSpeedMbps = 210,
                    Price = 6899M,
                    Type = DiskType.HDD,
                    Form = DiskForm.Sata35,
                }
            );
        }

        private static void SeedCity(MvcDiskContext context)
        {
            if (context.City.Any()) return;

            context.City.AddRange(
                new City { Name = "Москва" },
                new City { Name = "Санкт-Петербург" },
                new City { Name = "Новосибирск" },
                new City { Name = "Екатеринбург" },
                new City { Name = "Казань" },
                new City { Name = "Нижний Новгород" },
                new City { Name = "Челябинск" },
                new City { Name = "Самара" },
                new City { Name = "Омск" },
                new City { Name = "Ростов-на-Дону" },
                new City { Name = "Уфа" },
                new City { Name = "Красноярск" },
                new City { Name = "Воронеж" },
                new City { Name = "Пермь" },
                new City { Name = "Волгоград" }
            );
        }
    }
}
