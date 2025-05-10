using Microsoft.AspNetCore.Mvc;
using serviçohospital.Context;
using serviçohospital.Models;
using serviçohospital.Repository;
using System.Threading.Tasks;

namespace serviçohospital.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacienteController : ControllerBase
{
    private readonly IPacientesRepository _repository;

    public PacienteController(IPacientesRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("id")]
    public async Task<ActionResult<Paciente>> GetPacienteId(int id)
    {
        var paciente = await _repository.GetByIdAsync(id);
        if (paciente == null)
        {
            return NotFound($"Paciente com ID {id} não encontrado.");
        }
        return Ok(paciente);
    }

    [HttpGet("nome")]
    public async Task<ActionResult<Paciente>> GetPacienteNome(string nome)
    {
        var paciente = await _repository.BuscarPorNomeAsync(nome);
        if (paciente == null)
        {
            return NotFound($"Paciente com o nome {nome} não encontrado.");
        }
        return Ok(paciente);
    }

    [HttpPost]
    public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
    {
        if (paciente == null) { return BadRequest("Paciente não pode ser nulo."); }

        await _repository.CreateAsync(paciente);

        return CreatedAtAction(nameof(GetPacienteId), new { id = paciente.Id }, paciente);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Paciente>> UpdatePaciente(int id, Paciente paciente)
    {


        var pacienteExistente = await _repository.GetByIdAsync(id);
        if (pacienteExistente == null)
        {
            return NotFound("Paciente não encontrado.");
        }

        // Atualize as propriedades que você quer modificar
        pacienteExistente.Nome = paciente.Nome;
        pacienteExistente.CPF = paciente.CPF;
        pacienteExistente.DataNascimento = paciente.DataNascimento;
        pacienteExistente.Telefone = paciente.Telefone;

        // Agora, você chama o repositório para salvar as mudanças
        await _repository.UpdateAsync(pacienteExistente);

        return NoContent(); // Retorna um status 204 sem conteúdo, indicando que a atualização foi bem-sucedida.
    }
    [HttpDelete]
    public async Task<ActionResult<Paciente>> Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return Ok();

    }
}
