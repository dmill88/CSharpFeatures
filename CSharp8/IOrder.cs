using System;

namespace CSharp8
{
    public interface IOrder
    {
        DateTime Purchased { get; }
        decimal Cost { get; }
    }
}