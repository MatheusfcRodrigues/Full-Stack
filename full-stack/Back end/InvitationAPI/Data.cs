using InvitationAPI.Models;

namespace InvitationAPI.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Invitations.Any()) return;

        var invitations = new List<Invitation>
        {
            new Invitation
            {
                Name = "Matheus",
                Full = "Matheus Felipe Coelho Rodrigues",
                Date = "Abril 11 @ 00:44 am",
                Location = "Betim 2574",
                Category = "Painters",
                JobId = "5577421",
                Description = "Need to paint 2 aluminum windows and a sliding glass door",
                Price = "$62.00",
                Phone = "0412345678",
                Email = "matheus@example.com",
                Status = "Invited"
            },
            new Invitation
            {
                Name = "Vitória",
                Full = "Vitória Raquel Nunes Veloso",
                Date = "Abril 12 @ 1:15 am",
                Location = "Woolooware 2230",
                Category = "Interior Painters",
                JobId = "5588872",
                Description = "Internal walls 3 colours",
                Price = "$49.00",
                Phone = "04987654321",
                Email = "vitoria@example.com",
                Status = "Invited"
            }
        };

        context.Invitations.AddRange(invitations);
        context.SaveChanges();
    }
}
