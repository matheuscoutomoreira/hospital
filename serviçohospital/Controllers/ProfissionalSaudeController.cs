using Microsoft.AspNetCore.Mvc;
using serviçohospital.Context;
using serviçohospital.Models;
using serviçohospital.Repository;

namespace serviçohospital.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfissionalSaudeController : ControllerBase
    {
        private readonly IProfissionalDesaudeRepository _profissionalRepository;

        public ProfissionalSaudeController(IProfissionalDesaudeRepository repoprofissional)
        {
            _profissionalRepository = repoprofissional;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ProfissionalSaude>> GetProfissional(int id)
        {
            var consulta = await _profissionalRepository.GetByIdAsync(id);
            if (consulta is null) return NotFound("profissional não encontrado");
            return Ok(consulta);
        }

        [HttpGet("ObterHistoricoPaci/{id}")]


        public async Task<ActionResult<List<Historico>>> ObterhistoricoPaciente(int id)
        {
             var historico = await _profissionalRepository.ObterHistoricoPaciente(id);

            if (historico is null) return NotFound("historico vazio");

            return Ok(historico);
        }


        [HttpGet("ObterAgenda/{id}")]
        public async Task<ActionResult<List<Consulta>>> ObterAgenda(int profissionalId)
        {
            var agenda = await _profissionalRepository.ObterAgenda(profissionalId);
            if (agenda is null) return NotFound("agenda esta vazia");

            return Ok(agenda);
        }

        [HttpPost("EmitirPrescricao")]
        public async Task<ActionResult<Prescricao>> EmitirPrescricao(int consulta,string medicamento,string dosagem)
        {
             await _profissionalRepository.EmitirPrescricao(consulta,medicamento,dosagem);
            return Ok("criado com sucesso");
        }

        [HttpPut("FinalizarConsulta")]
        public  async Task<ActionResult<Consulta>> FinalizarConsulta(int consultaid, string prontuario)
        {
            await _profissionalRepository.FinalizarConsulta(consultaid, prontuario);
            return Ok();
        }

        
       
    }
    
}
