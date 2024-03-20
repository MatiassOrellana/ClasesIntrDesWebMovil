using Microsft.EntityFrameworkCore;


public class DataContext : DbContext {

    public DataContext(DbContextOptions<DataContext> options) : base(options) {



    }

    public DbSet<Task> Task { get; set; }


}