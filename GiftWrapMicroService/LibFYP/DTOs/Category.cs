namespace LibFYP.DTOs
{
    /// <summary>
    /// Class Category.
    /// </summary>
    /// <seealso cref="LibFYP.Dto" />
    public class Category : Dto
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int AvailableProductCount { get; set; }
    }
}