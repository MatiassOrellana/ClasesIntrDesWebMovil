using Microsoft.EntityFrameworkCore;


public class DataContext : DbContext {

    //para evitar las cosas repetitivas se usa el data context

    public DataContext(DbContextOptions<DataContext> options) : base(options) {



    }

    public DbSet<Task> Task { get; set; }


}