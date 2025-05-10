using Microsoft.AspNetCore.Mvc;
using serviçohospital.Context;

namespace serviçohospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalSaude:ControllerBase
    {
        private readonly AppDbContext _context;

    }
}
