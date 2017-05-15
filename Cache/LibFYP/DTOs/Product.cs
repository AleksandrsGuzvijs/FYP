using System;

namespace LibFYP.DTOs
{
    /// <summary>
    /// Class Product.
    /// </summary>
    /// <seealso cref="LibFYP.Dto" />
    public class Product : Dto
    {
        public virtual string Ean { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual int BrandId { get; set; }
        public virtual string BrandName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
        public virtual bool InStock { get; set; }
        public virtual DateTime ExpectedRestock { get; set; }
        public virtual string StoreName { get; set; }
    }
}