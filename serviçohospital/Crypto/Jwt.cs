using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using serviçohospital.Models;
namespace serviçohospital.Services;
public class JwtService
{
    private readonly string _chaveSecreta;
    private readonly double _expiracaoEmMinutos;

    public JwtService(string chaveSecreta, double expiracaoEmMinutos = 60)
    {
        _chaveSecreta = chaveSecreta;
        _expiracaoEmMinutos = expiracaoEmMinutos;
    }

    public string GerarToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Role, usuario.Tipo.ToString()) // Ex: "Paciente", "Profissional", "Administrador"
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chaveSecreta));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiracaoEmMinutos),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
