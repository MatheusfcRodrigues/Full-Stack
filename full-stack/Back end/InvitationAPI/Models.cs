using System.ComponentModel.DataAnnotations;

namespace InvitationAPI.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = "";

        [MaxLength(200)]
        public string Full { get; set; } = "";

        [MaxLength(50)]
        public string Date { get; set; } = "";

        [MaxLength(100)]
        public string Location { get; set; } = "";

        [MaxLength(100)]
        public string Category { get; set; } = "";

        [MaxLength(50)]
        public string JobId { get; set; } = "";

        [MaxLength(500)]
        public string Description { get; set; } = "";

        [MaxLength(20)]
        public string Price { get; set; } = "";

        [MaxLength(20)]
        public string Phone { get; set; } = "";

        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(20)]
        public string Status { get; set; } = "Invited";
    }
}