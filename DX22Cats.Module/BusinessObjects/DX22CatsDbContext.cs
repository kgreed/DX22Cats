using DevExpress.ExpressApp.EFCore.Updating;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.ExpressApp.Design;
using DevExpress.ExpressApp.EFCore.DesignTime;

namespace DX22Cats.Module.BusinessObjects;




// This code allows our Model Editor to get relevant EF Core metadata at design time.
// For details, please refer to https://supportcenter.devexpress.com/ticket/details/t933891.
public class DX22CatsContextInitializer : DbContextTypesInfoInitializerBase {
	protected override DbContext CreateDbContext() {
		var optionsBuilder = new DbContextOptionsBuilder<DX22CatsEFCoreDbContext>()
            .UseSqlServer(@";");
        return new DX22CatsEFCoreDbContext(optionsBuilder.Options);
	}
}
//This factory creates DbContext for design-time services. For example, it is required for database migration.
public class DX22CatsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<DX22CatsEFCoreDbContext> {
	public DX22CatsEFCoreDbContext CreateDbContext(string[] args) {
		throw new InvalidOperationException("Make sure that the database connection string and connection provider are correct. After that, uncomment the code below and remove this exception.");
		//var optionsBuilder = new DbContextOptionsBuilder<DX22CatsEFCoreDbContext>();
		//optionsBuilder.UseSqlServer(@"Integrated Security=SSPI;Pooling=false;Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DX22Cats");
		//return new DX22CatsEFCoreDbContext(optionsBuilder.Options);
	}
}
[TypesInfoInitializer(typeof(DX22CatsContextInitializer))]
public class DX22CatsEFCoreDbContext : DbContext {
	public DX22CatsEFCoreDbContext(DbContextOptions<DX22CatsEFCoreDbContext> options) : base(options) {
	}
	public DbSet<ModuleInfo> ModulesInfo { get; set; }
	public DbSet<Cat> Cats { get; set; }
}
