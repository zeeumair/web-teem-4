﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Webshop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public string PaymentOption { get; set; }
        public Double TotalAmount { get; set; }
        public string DeliveryOption { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Confirmed { get; set; } = false;
    }
}
