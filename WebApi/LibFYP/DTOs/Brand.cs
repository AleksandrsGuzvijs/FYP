namespace LibFYP.DTOs
{
    /// <summary>
    /// Class Brand.
    /// </summary>
    /// <seealso cref="LibFYP.Dto" />
    public class Brand : Dto
    {
        public virtual string Name { get; set; }
        public virtual int AvailableProductCount { get; set; }
    }
}