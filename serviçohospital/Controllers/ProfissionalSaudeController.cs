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



        [HttpPost]
        public async Task<ActionResult<ProfissionalSaude>> CreateProfissional([FromBody] ProfissionalSaude profissional)
        {
            if (profissional is null) return BadRequest("dados invalidos");

            await _profissionalRepository.Createasync(profissional);
            return Ok(profissional);
        }

        [HttpDelete]
        public async Task<ActionResult<ProfissionalSaude>> DeleteProfissional(int id)
        {

            var consulta = _profissionalRepository.GetByIdAsync(id);

            if (consulta is null) return NotFound("profissional não existe");


            await _profissionalRepository.DeleteAsync(id);
            return Ok(consulta + " foi apagado");
        }

        [HttpGet]
        public async Task<ActionResult<ProfissionalSaude>> GetProfissional(int id)
        {
            var consulta = await _profissionalRepository.GetByIdAsync(id);
            if (consulta is null) return NotFound("profissional não encontrado");
            return Ok(consulta);
        }

        [HttpGet("obterhistoricopaci")]


        public async Task<ActionResult<Historico>> ObterhistoricoPaciente(int id)
        {
             var historico = await _profissionalRepository.ObterHistoricoPaciente(id);

            if (historico is null) return NotFound("historico vazio");

            return Ok(historico);
        }


        [HttpGet("obteragenda")]
        public async Task<ActionResult<Consulta>> ObterAgenda(int profissionalId)
        {
            var agenda = await _profissionalRepository.ObterAgenda(profissionalId);
            if (agenda is null) return NotFound("agenda esta vazia");

            return Ok(agenda);
        }

        [HttpPost("emitirprescricao")]
        public async Task<ActionResult<Prescricao>> EmitirPrescricao(int consulta,string medicamento,string dosagem)
        {
            _profissionalRepository.EmitirPrescricao(consulta,medicamento,dosagem);
            return Ok("criado com sucesso");
        }

        [HttpPut("finalizar consulta")]
        public  async Task<ActionResult<Consulta>> FinalizarConsulta(int consultaid, string prontuario)
        {
             _profissionalRepository.FinalizarConsulta(consultaid, prontuario);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<ProfissionalSaude>> UpdateProfissional(int id, [FromBody] ProfissionalSaude profissionalSaude)
        {

            if (profissionalSaude == null)
            {
                return BadRequest("profissional não pode ser nulo.");
            }

            var profissionalExistente = await _profissionalRepository.GetByIdAsync(id);
            if (profissionalExistente == null)
            {
                return NotFound("Paciente não encontrado.");
            }

            // Atualiza as propriedades do paciente
            profissionalExistente.Nome = profissionalSaude.Nome;
            profissionalExistente.CRM = profissionalSaude.CRM;
            profissionalExistente.Especialidade = profissionalSaude.Especialidade;


            // Atualiza no repositório
            await _profissionalRepository.UpdateAsync(profissionalExistente);

            // Retorna o status 204 (NoContent) indicando sucesso
            return Ok();
        }
       
    }
    
}
