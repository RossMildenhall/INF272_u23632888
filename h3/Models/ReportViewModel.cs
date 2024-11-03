using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h3.Models
{
    public class ReportViewModel
    {
        public IEnumerable<students> students { get; set; }
        public IEnumerable<books> books { get; set; }
        public IEnumerable<borrows> borrows { get; set; }
    }
}