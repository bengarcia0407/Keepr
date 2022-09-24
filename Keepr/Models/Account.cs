namespace Keepr.Models
{
  public class Profile
    {
        public string Name { get; set; }
        public string Picture { get; set; }
    }
    public class Account : Profile
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}