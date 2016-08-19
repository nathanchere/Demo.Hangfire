using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Hangfire.Db
{
    [Table("InsuranceRegistry.FileRegistry")]
    public partial class FileRegistry
    {
        [Key]
        public int FileId { get; set; }

        public DateTime Created { get; set; }

        public int StatusId { get; set; }

        public virtual StatusType StatusType { get; set; }
    }
}
