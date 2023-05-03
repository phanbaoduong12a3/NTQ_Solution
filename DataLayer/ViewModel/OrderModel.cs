﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class OrderModel
    {
        public int ID { get; set; }
        public int? ProductID { get; set; }
        public int? UserID { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public double? Price { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set;}
        public DateTime? DeleteAt { get; set; }
        public int? Count { get; set; }
        public DateTime? EndAt { get; set; }
        public int? ShippingID { get; set; }
        public string Payment { get; set; }
        public string Address { get; set; }
        public int? Phone { get; set; }

    }
}
