using System;
using System.Collections.Generic;

namespace ShoppingCartAPI
{
    public partial class CustomerDetail
    {
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string StateName { get; set; }
    }

    public partial class OrderDetail
    {
        public string CityName { get; set; }
        public string PinCode { get; set; }
        public string StateName { get; set; }
        public decimal OrderQuantity { get; set; }
        public Int32 OrderItems { get; set; }
        public IList<ProductOrderDetail> orderProduct { get; set; }
    }

    public partial class ProductOrderDetail
    {
        public Nullable<decimal> Amount { get; set; }
        public string ProductName { get; set; }
    }
}