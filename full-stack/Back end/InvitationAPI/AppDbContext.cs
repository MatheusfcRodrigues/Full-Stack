using Microsoft.EntityFrameworkCore;
using InvitationAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Invitation> Invitations { get; set; }
}
