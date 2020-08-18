using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cognizant.Infrastructure.Data.PgSql.Challenges.Entities
{
    [Table("challenge")]
    public class Challenge
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("task_name")]
        public string TaskName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Column("input_param")]
        public string InputParam { get; set; }

        [Column("output_param")]
        public string OutputParam { get; set; }
    }
}
