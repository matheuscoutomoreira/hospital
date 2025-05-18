using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serviçohospital.Models;
using serviçohospital.Repository;
using System;
using System.Threading.Tasks;

namespace serviçohospital.Controllers;
[Authorize(Roles = "Paciente")]
[ApiController]
[Route("api/[controller]")]
public class PacienteController : ControllerBase
{
    private readonly IPacientesRepository _repository;
    private readonly IProfissionalDesaudeRepository _profissionalDesaudeRepository;

    public PacienteController(IPacientesRepository repository, IProfissionalDesaudeRepository profissionalDesaudeRepository)
    {
        _repository = repository;
        _profissionalDesaudeRepository = profissionalDesaudeRepository;
    }

    // GET: api/paciente/{id}
    [HttpGet("GetPacienteId/{id}")]
    public async Task<ActionResult<Paciente>> GetPacienteId(int id)
    {
        var paciente = await _repository.GetByIdAsync(id);
        if (paciente == null) return NotFound();

        try
        {
            if (!string.IsNullOrWhiteSpace(paciente.CPF))
                paciente.CPF = CriptografiaHelper.Descriptografar(paciente.CPF);

            if (!string.IsNullOrWhiteSpace(paciente.Telefone))
                paciente.Telefone = CriptografiaHelper.Descriptografar(paciente.Telefone);
        }
        catch
        {
            return BadRequest("Erro ao descriptografar os dados. Verifique se estão corretamente armazenados.");
        }

        return Ok(paciente);
    }

    // GET: api/paciente/por-nome/{nome}
    [HttpGet("por-nome/{nome}")]
    public async Task<ActionResult<Paciente>> GetPacientePorNome(string nome)
    {
        var paciente = await _repository.BuscarPorNomeAsync(nome);
        if (paciente == null)
            return NotFound($"Paciente com o nome {nome} não encontrado.");
        try
        {
            if (!string.IsNullOrWhiteSpace(paciente.CPF))
                paciente.CPF = CriptografiaHelper.Descriptografar(paciente.CPF);

            if (!string.IsNullOrWhiteSpace(paciente.Telefone))
                paciente.Telefone = CriptografiaHelper.Descriptografar(paciente.Telefone);
        }
        catch
        {
            return BadRequest("Erro ao descriptografar os dados. Verifique se estão corretamente armazenados.");
        }
        return Ok(paciente);
    }

    // POST: api/paciente
    [HttpPost("CriaPaciente")]
    public async Task<ActionResult<Paciente>> PostPaciente([FromBody] Paciente paciente)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(paciente.Nome) || paciente.Nome == "string")
            return BadRequest("Nome é obrigatório e não pode ser 'string'.");

        if (string.IsNullOrWhiteSpace(paciente.CPF) || paciente.CPF == "string")
            return BadRequest("CPF é obrigatório e não pode ser 'string'.");

        if (string.IsNullOrWhiteSpace(paciente.Telefone) || paciente.Telefone == "string")
            return BadRequest("Telefone é obrigatório e não pode ser 'string'.");

        paciente.CPF = CriptografiaHelper.Criptografar(paciente.CPF);
        paciente.Telefone = CriptografiaHelper.Criptografar(paciente.Telefone);

        paciente.Historico = new Historico
        {
            Data = DateTime.Now
        };

        await _repository.CreateAsync(paciente);

        // Evita retornar os dados criptografados na resposta
        paciente.CPF = null;
        paciente.Telefone = null;

        return CreatedAtAction(nameof(GetPacienteId), new { id = paciente.Id }, paciente);
    }

    // PUT: api/paciente/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarPaciente(int id, [FromBody] Paciente paciente)
    {
        if (paciente == null)
            return BadRequest("Paciente não pode ser nulo.");

        var existente = await _repository.GetByIdAsync(id);
        if (existente == null)
            return NotFound("Paciente não encontrado.");

        existente.Nome = paciente.Nome;
        existente.CPF = CriptografiaHelper.Criptografar(paciente.CPF);
        existente.DataNascimento = paciente.DataNascimento;
        existente.Telefone = CriptografiaHelper.Criptografar(paciente.Telefone);

        await _repository.UpdateAsync(existente);
        return NoContent();
    }

    // DELETE: api/paciente/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> ApagarPaciente(int id)
    {
        var existente = await _repository.GetByIdAsync(id);
        if (existente == null)
            return NotFound("Paciente não encontrado.");

        await _repository.DeleteAsync(id);
        return Ok();
    }

    // PATCH: api/paciente/consulta/{id}/cancelar
    [HttpPatch("consulta/{id}/cancelar")]
    public async Task<ActionResult<Consulta>> CancelarConsulta(int id)
    {
        var consulta = await _repository.GetConsultaByIdAsync(id);
        if (consulta == null)
            return NotFound("Consulta não encontrada.");

        await _repository.CancelarConsultaAsync(id);
        return Ok(consulta);
    }

    // POST: api/paciente/consulta
    [HttpPost("consulta")]
    public async Task<ActionResult<Consulta>> CriarConsulta([FromBody] CriarConsultaDTO dto)
    {
        var paciente = await _repository.GetByIdAsync(dto.PacienteId);
        if (paciente == null)
            return NotFound("Paciente não encontrado.");

        var profissional = await _profissionalDesaudeRepository.GetByIdAsync(dto.ProfissionalSaudeId);
        if (profissional == null)
            return NotFound("Profissional não encontrado.");

        var novaConsulta = new Consulta
        {
            DataHora = dto.DataConsulta,
            PacienteId = dto.PacienteId,
            ProfissionalSaudeId = dto.ProfissionalSaudeId,
            Observacoes = dto.Observacoes,
            Status = StatusConsulta.Agendado
        };

        await _repository.CriarConsultaAsync(novaConsulta);
        return CreatedAtAction(nameof(CriarConsulta), new { id = novaConsulta.Id }, novaConsulta);
    }

    // GET: api/paciente/{id}/historico
    [HttpGet("{id}/historico")]
    public async Task<ActionResult<Historico>> GetHistorico(int id)
    {
        var paciente = await _repository.GetByIdAsync(id);
        if (paciente == null)
            return NotFound("Paciente não encontrado.");

        var historico = await _repository.GetHistoricoAsync(id);
        return Ok(historico);
    }
}
