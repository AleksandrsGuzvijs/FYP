namespace LibFYP.DTOs
{
    /// <summary>
    /// Class User.
    /// </summary>
    /// <seealso cref="LibFYP.Dto" />
    public class User : Dto
    {
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }
}