using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace BlazorServerDemoApp.Models
{
    public class LedgerInputModel
    {
        public int Year { get; set; }
        public decimal Budget { get; set; }
        public decimal Actual { get; set; }
        public AccountModel Account { get; set; }
        public decimal Variance { get; set; }

    }
}
