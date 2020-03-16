using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8
{
    public class SaleOrder: IOrder
    {
        public SaleOrder(DateTime purchase, decimal cost) =>
            (Purchased, Cost) = (purchase, cost);

        public DateTime Purchased { get; }

        public decimal Cost { get; }
    }
}
