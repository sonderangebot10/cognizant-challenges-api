using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cognizant.Infrastructure.Data.PgSql.Challenges.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("name")]
        public string Name { get; set; }

        [Column("success_solutions")]
        public int SuccessSolutions { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("tasks")]
        public string Tasks { get; set; }
    }
}