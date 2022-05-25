using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductAssignment.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public double Price { get; set; }

        public DateTime MFGDate { get;set; }

        public String MFGDateFormatted { get { return String.Format("{0:dd/MM/yyyy}", MFGDate); } }
        public string Category { get; set; }

        public string SearchWith { get; set; }

    }
}