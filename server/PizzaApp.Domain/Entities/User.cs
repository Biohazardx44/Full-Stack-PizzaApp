using Microsoft.AspNetCore.Identity;

namespace PizzaApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public bool FirstLogin { get; set; }
    }
}