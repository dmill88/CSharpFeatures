using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp7
{
    public class TuplePoint
    {
        public TuplePoint(double x, double y)
            => (X, Y) = (x, y);

        public double X { get; }
        public double Y { get; }

        public void Deconstruct(out double x, out double y) =>
            (x, y) = (X, Y);
    }
}
