
using Microsoft.AspNetCore.Mvc;
using serviçohospital.Context;

namespace serviçohospital.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PacienteController:ControllerBase
{
    private readonly AppDbContext _context;

    public PacienteController(AppDbContext context)
    {
        _context = context; 
    }
}
