using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Hangfire.Db
{
    [Table("InsuranceRegistry.StatusType")]
    public class StatusType
    {                
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }        
    }
}
