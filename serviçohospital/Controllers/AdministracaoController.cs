using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serviçohospital.Models;
using serviçohospital.Repository;

namespace serviçohospital.Controllers;
[Authorize(Roles = "Administrador")]
[ApiController]
[Route("api/[controller]")]
public class Administracao : ControllerBase
{
    private readonly IAdministradorRepository _repository;
    public Administracao(IAdministradorRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("CriarProfissional")]
    public async Task<ActionResult<ProfissionalSaude>> CriarProfissional([FromBody] ProfissionalSaude profissional)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(profissional.Nome) || profissional.Nome.Trim().ToLower() == "string")
            return BadRequest("Nome é obrigatório e não pode ser 'string'.");

        if (string.IsNullOrWhiteSpace(profissional.CRM) || profissional.CRM.Trim().ToLower() == "string")
            return BadRequest("CRM é obrigatório e não pode ser 'string'.");

        if (string.IsNullOrWhiteSpace(profissional.Especialidade) || profissional.Especialidade.Trim().ToLower() == "string")
            return BadRequest("Especialidade é obrigatória e não pode ser 'string'.");

        // Criptografar dados sensíveis antes de salvar
        profissional.CRM = CriptografiaHelper.Criptografar(profissional.CRM);

        await _repository.CriarProfissionalAsync(profissional);

        // Descriptografar para retornar ao cliente
        profissional.CRM = CriptografiaHelper.Descriptografar(profissional.CRM);

        return CreatedAtAction(nameof(GetProfissional), new { id = profissional.Id }, profissional);
    }

    [HttpPatch("CancelarConsulta")]
    public async Task<ActionResult> CancelarConsulta(int idconsulta)
    {
        await _repository.CancelarConsultaAsync(idconsulta);
        return Ok("Consulta cancelada com sucesso");
    }

    [HttpDelete("ApagarPaciente")]
    public async Task<ActionResult> ApagarPaciente(int id)
    {
        if (id == 0) return BadRequest("Digite um número válido");
        await _repository.DesativarPacienteAsync(id);
        return NoContent();
    }

    [HttpDelete("ApagarProfissional")]
    public async Task<ActionResult> Apagarprofissional(int id)
    {
        if (id == 0) return BadRequest("Digite um número válido");
        await _repository.DesativarProfissionalAsync(id);
        return NoContent();
    }

    [HttpPatch("EditarPaciente")]
    public async Task<ActionResult> EditarPaciente([FromBody] Paciente paciente)
    {
        if (paciente == null) return BadRequest("Paciente inválido");

        // Criptografar dados sensíveis
        paciente.CPF = CriptografiaHelper.Criptografar(paciente.CPF);
        paciente.Telefone = CriptografiaHelper.Criptografar(paciente.Telefone);

        var atualizado = await _repository.EditarPacienteAsync(paciente);
        if (atualizado == null)
            return NotFound("Paciente não encontrado.");

        return Ok("Paciente alterado com sucesso.");
    }

    [HttpPatch("EditarProfissional")]
    public async Task<ActionResult> EditarProfissional([FromBody] ProfissionalSaude profissionalSaude)
    {
        if (profissionalSaude == null) return BadRequest("Profissional inválido");

        // Criptografar dados sensíveis
        profissionalSaude.CRM = CriptografiaHelper.Criptografar(profissionalSaude.CRM);

        var atualizado = await _repository.EditarProfissionalAsync(profissionalSaude);
        if (atualizado == null)
            return NotFound("Profissional não encontrado.");

        return Ok("Profissional alterado com sucesso.");
    }

    [HttpGet("GetProfissional")]
    public async Task<ActionResult<ProfissionalSaude>> GetProfissional(int id)
    {
        if (id == 0) return BadRequest("Número inválido");

        var profissional = await _repository.GetProfissional(id);
        if (profissional == null) return NotFound("Profissional não encontrado.");

      
        profissional.CRM = CriptografiaHelper.Descriptografar(profissional.CRM);

        return Ok(profissional);
    }

    [HttpGet("GetListaConsultas")]
    public async Task<ActionResult<List<Consulta>>> GetConsultaslista()
    {
        var listaconsultas = await _repository.ListarConsultasAsync();
        return Ok(listaconsultas.ToList());
    }

    [HttpGet("GetlistaPaciente")]
    public async Task<ActionResult<List<Paciente>>> Getpacienteslista()
    {
        var listapacientes = await _repository.ListarPacientesAsync();
        if (listapacientes == null) return NotFound("Pacientes não existem");

     
        foreach (var paciente in listapacientes)
        {
            paciente.CPF = CriptografiaHelper.Descriptografar(paciente.CPF);
            paciente.Telefone = CriptografiaHelper.Descriptografar(paciente.Telefone);
        }

        return Ok(listapacientes.ToList());
    }

    [HttpGet("GetlistaProfissionais")]
    public async Task<ActionResult<List<ProfissionalSaude>>> Getprofissionaislista()
    {
        var listaprofissionais = await _repository.ListarProfissionaisAsync();
        if (listaprofissionais == null) return NotFound("Profissionais não existem");

        // Descriptografar CRM para cada profissional
        foreach (var profissional in listaprofissionais)
        {
            profissional.CRM = CriptografiaHelper.Descriptografar(profissional.CRM);
        }

        return Ok(listaprofissionais.ToList());
    }
}
