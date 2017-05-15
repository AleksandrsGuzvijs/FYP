using System;

namespace LibFYP.DTOs
{
    /// <summary>
    /// Class Order.
    /// </summary>
    /// <seealso cref="LibFYP.Dto" />
    public class Order : Dto
    {
        public virtual string AccountName { get; set; }
        public virtual string CardNumber { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime When { get; set; }
        public virtual string ProductName { get; set; }
        public virtual string ProductEan { get; set; }
        public virtual double TotalPrice { get; set; }
    }
}