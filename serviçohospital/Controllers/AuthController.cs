using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using serviçohospital.Context;
using serviçohospital.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using serviçohospital.Crypto;
using serviçohospital.Services; 

namespace serviçohospital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly JwtService _jwtService;

    public AuthController(AppDbContext context, IConfiguration configuration, JwtService jwtService)
    {
        _context = context;
        _configuration = configuration;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
            return BadRequest("Email e senha são obrigatórios.");

        if (_context.Usuarios.Any(u => u.Email == request.Email))
            return BadRequest("Email já cadastrado.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Senha);

        var usuario = new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            Senha = hashedPassword,
            Tipo = request.Tipo
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDTO login)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == login.Email);
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha))
            return Unauthorized("Credenciais inválidas.");

        var token = _jwtService.GerarToken(usuario);
        return Ok(new { token });
    }
}
