using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serviçohospital.Models;
using serviçohospital.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace serviçohospital.Controllers
{
    [Authorize(Roles = "Profissional")]
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
            var profissional = await _profissionalRepository.GetByIdAsync(id);
            if (profissional is null)
                return NotFound("Profissional não encontrado");

            try
            {
                if (!string.IsNullOrWhiteSpace(profissional.CRM))
                    profissional.CRM = CriptografiaHelper.Descriptografar(profissional.CRM);
            }
            catch
            {
                return BadRequest("Erro ao descriptografar os dados do profissional.");
            }

            return Ok(profissional);
        }

        [HttpGet("ObterHistoricoPaci/{id}")]
        public async Task<ActionResult<List<Historico>>> ObterhistoricoPaciente(int id)
        {
            var historico = await _profissionalRepository.ObterHistoricoPaciente(id);

            if (historico is null)
                return NotFound("Histórico vazio");

            try
            {
                // Se precisar descriptografar dados no histórico, faça aqui.
                // Exemplo:
                // foreach(var item in historico)
                // {
                //     if (!string.IsNullOrWhiteSpace(item.CampoCriptografado))
                //         item.CampoCriptografado = CriptografiaHelper.Descriptografar(item.CampoCriptografado);
                // }
            }
            catch
            {
                return BadRequest("Erro ao descriptografar os dados do histórico.");
            }

            return Ok(historico);
        }

        [HttpGet("ObterAgenda/{profissionalId}")]
        public async Task<ActionResult<List<Consulta>>> ObterAgenda(int profissionalId)
        {
            var agenda = await _profissionalRepository.ObterAgenda(profissionalId);
            if (agenda is null)
                return NotFound("Agenda está vazia");

            try
            {
                // Se precisar descriptografar dados nas consultas da agenda, faça aqui.
                // Exemplo:
                // foreach(var consulta in agenda)
                // {
                //     if (!string.IsNullOrWhiteSpace(consulta.CampoCriptografado))
                //         consulta.CampoCriptografado = CriptografiaHelper.Descriptografar(consulta.CampoCriptografado);
                // }
            }
            catch
            {
                return BadRequest("Erro ao descriptografar os dados da agenda.");
            }

            return Ok(agenda);
        }

        [HttpPost("EmitirPrescricao")]
        public async Task<ActionResult> EmitirPrescricao(int consulta, string medicamento, string dosagem)
        {
            await _profissionalRepository.EmitirPrescricao(consulta, medicamento, dosagem);
            return Ok("Prescrição criada com sucesso");
        }

        [HttpPut("FinalizarConsulta")]
        public async Task<ActionResult> FinalizarConsulta(int consultaid, string prontuario)
        {
            await _profissionalRepository.FinalizarConsulta(consultaid, prontuario);
            return Ok("Consulta finalizada com sucesso");
        }
    }
}
