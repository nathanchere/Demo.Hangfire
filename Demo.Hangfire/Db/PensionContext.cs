using System.Data.Entity;

namespace Demo.Hangfire.Db
{
    public interface IPensionContext
    {
        DbSet<FileRegistry> FileRegistries { get; set; }          
    }

    public class PensionContext : DbContext, IPensionContext
    {
        public PensionContext()
            : base("name=PensionDb")
        {
        }

        public virtual DbSet<FileRegistry> FileRegistries { get; set; }        
    }
}
