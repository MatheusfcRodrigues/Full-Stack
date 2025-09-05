using InvitationAPI.Data;
using InvitationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvitationAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvitationsController : ControllerBase
{
    private readonly AppDbContext _context;
    public InvitationsController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _context.Invitations.ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Invitation invite)
    {
        _context.Invitations.Add(invite);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), new { id = invite.Id }, invite);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
    {
        var invite = await _context.Invitations.FindAsync(id);
        if (invite == null) return NotFound();
        invite.Status = status;
        await _context.SaveChangesAsync();
        return Ok(invite);
    }

private void notificationMessage(string email, string message)
{
    // Simula o envio de notificação 
    System.IO.File.AppendAllText("notificacoes.txt", $"Para: {email} - {message}\n");
}

   [HttpPut("{id}/accept")]
public IActionResult Accept(int id)
{
    var invitation = _context.Invitations.Find(id);
    if (invitation == null) return NotFound();

    // Remover símbolos e espaços do preço
    var cleanPrice = new string(invitation.Price.Where(char.IsDigit).ToArray());
    if (decimal.TryParse(cleanPrice, out decimal price) && price > 500)
    {
        price = price * 0.9m; // Aplica 10% de desconto
        invitation.Price = price.ToString("0.##");
        notificationMessage("vendas@test.com", "Preço acima de 500 foi aplicado o desconto de 10%");
    }

    invitation.Status = "Accepted";
    _context.SaveChanges();

    // Simular o envido de notificação
    System.IO.File.AppendAllText("notificacoes.txt", $"Convite aceito: {invitation.Name}, valor: {invitation.Price}\n");

    return NoContent();
}

[HttpPut("{id}/decline")]
public IActionResult Decline(int id)
{
    var invitation = _context.Invitations.Find(id);
    if (invitation == null) return NotFound();

    invitation.Status = "Declined";
    _context.SaveChanges();

    notificationMessage("vendas@test.com", $"Convite recusado: {invitation.Name}");

    return NoContent();
}

}