using Microsoft.AspNetCore.Identity;
namespace projetoepi.Models;
public class ApplicationUser : IdentityUser{
    public decimal Cpf {get; set;}
}