using System.Drawing.Printing;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.IdentityModel.Tokens;
using projetoepi.Context;
using projetoepi.Models;
namespace apisaude.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    public AdminController(UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration, AppDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
    }
    [HttpGet]
    public ActionResult<string> Get()
    {
        return " << Controlador UsuariosController :: WebApiUsuarios >>";
    }
    [HttpPost("Criar")]
    public async Task<ActionResult<UserToken>> CreateUser([FromBody]
UserInfo model)
    {
        var pacienteCad = _context.Colaboradors.FirstOrDefault(e => e.Email
        == model.Email && e.Cpf == model.Cpf);
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Cpf = model.Cpf
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                var roles = await _userManager.GetRolesAsync(user);
                return BuildToken(model, roles);
            }
            else
            {
                return BadRequest("Usuário ou senha inválidos");
            }

    }
    private UserToken BuildToken(UserInfo userInfo, IList<string> userRoles)
    {
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
        new Claim("meuValor", "oque voce quiser"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var key = new
        SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
        var creds = new SigningCredentials(key,
        SecurityAlgorithms.HmacSha256);
        // tempo de expiração do token: 1 hora
        var expiration = DateTime.UtcNow.AddHours(1);
        JwtSecurityToken token = new JwtSecurityToken(
        issuer: null,
        audience: null,
        claims: claims,
        expires: expiration,
        signingCredentials: creds);

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration,
            Roles = userRoles
        };
    }
}

