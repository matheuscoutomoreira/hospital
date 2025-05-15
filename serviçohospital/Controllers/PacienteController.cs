using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using serviçohospital.Context;
using serviçohospital.Models;
using serviçohospital.Repository;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;

namespace serviçohospital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacienteController : ControllerBase
{
    private readonly IPacientesRepository _repository;
    private readonly IProfissionalDesaudeRepository _profissionalDesaudeRepository;

    public PacienteController(IPacientesRepository repository,IProfissionalDesaudeRepository profissionalDesaudeRepository)
    {
        _repository = repository;
        _profissionalDesaudeRepository = profissionalDesaudeRepository;
    }


    //pega o apciente pelo id
    [HttpGet("getPacienteId")]
    public async Task<ActionResult<Paciente>> GetPacienteId(int id)
    {
        var paciente = await _repository.GetByIdAsync(id);
        if (paciente == null)
        {
            return NotFound($"Paciente com ID {id} não encontrado.");
        }
        return Ok(paciente);
    }


    //pega o paciente pelo nome
    [HttpGet("GetPacienteNome")]
    public async Task<ActionResult<Paciente>> GetPacienteNome(string nome)
    {
        var paciente = await _repository.BuscarPorNomeAsync(nome);
        if (paciente == null)
        {
            return NotFound($"Paciente com o nome {nome} não encontrado.");
        }
        return Ok(paciente);
    }

    //cria o paciente
    [HttpPost("CriaPaciente")]

    public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
    {
        if (paciente is null)return BadRequest("Paciente não pode ser nulo.");

        // Cria um histórico vazio vinculado ao paciente
        paciente.Historico = new Historico
        {
            Data = DateTime.Now,
            PacienteId = paciente.Id // será atribuído automaticamente pelo EF
        };

        await _repository.CreateAsync(paciente);

        return CreatedAtAction(nameof(GetPacienteId), new { id = paciente.Id }, paciente);
    }




    [HttpPut("{id}/Atualizapaciente")]
    public async Task<ActionResult<Paciente>> UpdatePaciente(int id, [FromBody] Paciente paciente)
    {
        if (paciente == null)
        {
            return BadRequest("Paciente não pode ser nulo.");
        }

        var pacienteExistente = await _repository.GetByIdAsync(id);
        if (pacienteExistente == null)
        {
            return NotFound("Paciente não encontrado.");
        }

        // Atualiza as propriedades do paciente
        pacienteExistente.Nome = paciente.Nome;
        pacienteExistente.CPF = paciente.CPF;
        pacienteExistente.DataNascimento = paciente.DataNascimento;
        pacienteExistente.Telefone = paciente.Telefone;

        // Atualiza no repositório
        await _repository.UpdateAsync(pacienteExistente);

        // Retorna o status 204 (NoContent) indicando sucesso
        return NoContent();
    }

    [HttpDelete("{id}/ApagaPaciente")]
    public async Task<ActionResult<Paciente>> Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }
        var PacienteExistente =  await _repository.GetByIdAsync(id);
        if (PacienteExistente is null) return NotFound("cliente não encontrado");


        await _repository.DeleteAsync(id);
        return Ok();

    }

    [HttpPatch("CancelaConsulta")]
    public async Task<ActionResult<Consulta>> CancelarConsulta(int id)
    {
        var consulta = await _repository.GetByIdAsync(id);
        if (consulta == null) return BadRequest("consulta não existe");

         await _repository.CancelarConsultaAsync(id);
        return Ok(consulta);
    }

    [HttpPost("criarconsulta")]
    public async Task<ActionResult<Consulta>> criarConsulta(CriarConsultaDTO dto)
    {
       var paciente = await _repository.GetByIdAsync(dto.PacienteId);
        if (paciente is null) return NotFound("paciente não encontrado");

        var profissional = await _profissionalDesaudeRepository.GetByIdAsync(dto.ProfissionalSaudeId);
        Console.WriteLine(profissional);
        if (profissional is null) return NotFound("profissional não encontrado");
        var novaConsulta = new Consulta
        {
            DataHora = dto.DataConsulta,
            PacienteId = dto.PacienteId,
            ProfissionalSaudeId = dto.ProfissionalSaudeId,
            Observacoes = dto.Observacoes,
            Status = StatusConsulta.Agendado


        }
        ;
         await _repository.CriarConsultaAsync(novaConsulta);
        return CreatedAtAction(nameof(criarConsulta), new { id = novaConsulta.Id }, novaConsulta);
    }

    [HttpGet("getHistorico")]
    public async Task<ActionResult<Historico>> GetHistorico(int id)
    {
       var paciente =  await _repository.GetByIdAsync(id);

        if (paciente == null) return NotFound("pacinete não existe");

        var resultado = await _repository.GetHistoricoAsync(id);
        return Ok(resultado);

    }
}
