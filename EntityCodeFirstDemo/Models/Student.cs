using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EntityCodeFirstDemo.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int RollNumber { get; set; }

        [Required]
        [Column("StudentName", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [Required]
        // [Index("Ix_Student_Email", IsClustered = false, IsUnique = true)]
        public string Email { get; set; }

        [NotMapped]
        public string ConfirmEmail { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }

        public bool IsActive { get; set; }

        public Trainer Trainer { get; set; }
    }
}