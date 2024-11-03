using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace h3.Models
{
    public class CombinedViewModel
    {
        public IEnumerable<students> students { get; set; }
        public IEnumerable<books> books { get; set; }
        public IEnumerable<authors> authors { get; set; }
        public IEnumerable<types> types { get; set; }
        public IEnumerable<borrows> borrows { get; set; }

    }
}