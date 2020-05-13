using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.Models
{

    [NotMapped]
    public class CurrencyRates
    {
        public int Id { get; set; }

        public Dictionary<string, double> Rates { get; set; }

        public string Base { get; set; }

        public DateTimeOffset Date { get; set; }
    }
}