using System;
using System.Collections.Generic;
using System.Text;

namespace NetShop
{
    public partial class Bill
    {
        public int Id { get; set; }
        public DateTime DateBill { get; set; }
        public string Orders { get; set; }
        public int WhoOrdered { get; set; }
        public bool IsDelivery { get; set; }
    }
}
