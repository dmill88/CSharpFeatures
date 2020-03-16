using System;

namespace CSharp6
{
    /// <summary>
    /// Read-only, immutable type. 
    /// Uses nameof expression
    /// </summary>
    public class MyImmutableType
    {
        public MyImmutableType(decimal amountDeposit, DateTime dateTimeDeposited)
        {
            if (amountDeposit <= decimal.Zero)
                throw new ArgumentException(message: "Must be greater than zero.", paramName: nameof(amountDeposit));
            if (dateTimeDeposited == DateTime.MinValue)
                throw new ArgumentException(message: "Must be greater than the minimum DateTime.", paramName: nameof(dateTimeDeposited));

            AmountDeposit = amountDeposit;
            DateTimeDeposited = dateTimeDeposited;
        }

        /// <summary>
        /// Read-only auto-properties
        /// </summary>
        public decimal AmountDeposit { get; }
        public DateTime DateTimeDeposited { get; }

        public override string ToString() => $"{AmountDeposit} {DateTimeDeposited.ToString()}";

    }

}
