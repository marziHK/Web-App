using System.ComponentModel.DataAnnotations;

namespace HosseinkhaniTest.Models
{
    public class Tbl_PersonnelDocument
    {
        [Key]
        public int Id { get; set; }

        public string? Source { get; set; }

        public int FK_PersonnelId { get; set; }

        public virtual Tbl_Personnels Personnel { get; set; } = null!;
    }
}
