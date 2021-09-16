using System.Collections.Generic;

namespace Mvc.Models
{
    public class DiskListViewModel
    {
        public List<Disk> Disks;
        public string SortBy;
        public string SortDirection;
        public string Search;
    }
}
