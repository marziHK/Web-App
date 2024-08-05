
using System.ComponentModel.DataAnnotations;

namespace HosseinkhaniTest.Models
{
    public class Tbl_Personnels
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [MaxLength(10)]
        public int NationalCode { get; set; }

        [MinLength(4)]
        [MaxLength(10)]
        public string? PersonalCode { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? LastDateModified { get; set; }

        // 0: Remove; 1: Insert; 2:Update
        public int State { get; set; }


        //For Foreignkey
        public virtual ICollection<Tbl_PersonnelDocument> Tbl_PersonnelDocument { get; set; } = new List<Tbl_PersonnelDocument>();
    }
}
