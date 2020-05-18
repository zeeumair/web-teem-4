using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string CurrencyCode { get; set; }

        public double CurrencyRate { get; set; }

        public DateTimeOffset LastUpdated { get; set; }
    }
}
