using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


public delegate void SeedDataDelegate(ModelBuilder modelBuilder);

public class MaDal : IdentityDbContext<AccountDAO, RoleDAO, Guid>
{
    private readonly SeedDataDelegate? seedData;

    public MaDal(
        DbContextOptions<MaDal> options,
        SeedDataDelegate? seedData = null
    ) : base(options)
    {
        this.seedData = seedData;
        this.Database.EnsureCreated();
    }

    public DbSet<LoanDAO> Loans { get; set; }
    public DbSet<ObjectDAO> Objects { get; set; }
    public DbSet<PhotoDAO> Photos { get; set; }

    public DbSet<AppUserDAO> AppUsers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LoanDAO>(entity =>
        {
            entity.HasKey(c => c.IdLoan);
        });

        modelBuilder.Entity<PhotoDAO>(entity =>
        {
            entity.HasKey(c => c.IdPhoto);
        });

        modelBuilder.Entity<AppUserDAO>(entity =>
        {
            entity.HasKey(c => c.IdUser);
            entity.HasMany(c => c.Objects).WithOne(c => c.Owner).HasForeignKey(c => c.IdOwner);
            entity.HasMany(c => c.Loans).WithOne(c => c.Borrower).HasForeignKey(c => c.IdBorrower);
            entity.HasOne(c => c.Account).WithOne(c => c.AppUser).HasForeignKey<AppUserDAO>(c => c.IdUser);// HasForeignKey(c=>c.IdUser) ne fonctionne pas

        });

        modelBuilder.Entity<ObjectDAO>(entity =>
        {
            entity.HasKey(c => c.IdObject);
            entity.HasMany(c => c.Loans).WithOne(c => c.Object).HasForeignKey(c => c.IdObject).OnDelete(DeleteBehavior.NoAction);
            entity.HasMany(c => c.Photos).WithOne(c => c.Object).HasForeignKey(c => c.IdObject).OnDelete(DeleteBehavior.NoAction);

        });
        var ac1 = new AccountDAO() { UserName = "Gab" };
        var ac2 = new AccountDAO() { UserName = "Jul" };
        var ac3 = new AccountDAO() { UserName = "Ben" };

       

        modelBuilder.Entity<AccountDAO>().HasData(new List<AccountDAO>() { ac1, ac2, ac3 });

        var u1 = new AppUserDAO() { IdUser = ac1.Id, FirstName = "Gabriel", LastName = "Dupond", PhoneNumber = "0546659845", City = "Bordeaux" };
        var u2 = new AppUserDAO() { IdUser = ac2.Id, FirstName = "Julie", LastName = "Gay", PhoneNumber = "05466654845", City = "Bazas" };
        var u3 = new AppUserDAO() { IdUser = ac3.Id, FirstName = "Benoit", LastName = "Sab", PhoneNumber = "054123845", City = "Embrun" };




        modelBuilder.Entity<AppUserDAO>().HasData(new List<AppUserDAO>() { u1, u2, u3 });

        var o1 = new ObjectDAO()
        {
            Label = "Pelle",
            Description = "Pelle en fer inoxidable",
            EstimatedPrice = 19.99M,
            IdOwner = u3.IdUser
        };

        var o4 = new ObjectDAO()
        {
            Label = "Pioche",
            Description = "En tres bon état",
            EstimatedPrice = 19.99M,
            IdOwner = u3.IdUser
        };

        var o5 = new ObjectDAO()
        {
            Label = "Betonniere",
            Description = "a servi et peut encore servir",
            EstimatedPrice = 99.99M,
            IdOwner = u3.IdUser
        };


        var o2 = new ObjectDAO()
        {
            Label = "Tractopelle",
            Description = "Avec un godet de qualité",
            EstimatedPrice = 1200M,
            IdOwner = u1.IdUser
        };

        var o6 = new ObjectDAO()
        {
            Label = "perçeuse",
            Description = "Perceuse à percussion filaire BOSCH Universalimpact",
            EstimatedPrice = 50M,
            IdOwner = u1.IdUser
        };
        var o7 = new ObjectDAO()
        {
            Label = "Truelle",
            Description = "Truelle trapézoïdale en acier inoxydable, soudée",
            EstimatedPrice = 5.99M,
            IdOwner = u1.IdUser
        };

        var o3 = new ObjectDAO()
        {
            Label = "Aspirateur",
            Description = "Qui ne perd son soufle",
            EstimatedPrice = 145.32M,
            IdOwner = u2.IdUser
        };

         var o8 = new ObjectDAO()
        {
            Label = "Machine à coudre",
            Description = "Quasi neuve",
            EstimatedPrice = 159.32M,
            IdOwner = u2.IdUser
        };


         var o9 = new ObjectDAO()
        {
            Label = "Télescope",
            Description = "Idéale pour observer les étoiles",
            EstimatedPrice = 75.32M,
            IdOwner = u2.IdUser
        };




        modelBuilder.Entity<ObjectDAO>().HasData(new List<ObjectDAO>() { o1, o2, o3, o4 , o5 , o6 , o7 ,o8 ,o9});

        var p1 = new PhotoDAO(){IdObject = o1.IdObject, Path = "https://images.unsplash.com/photo-1599914466149-1b958674b7ba?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8M3x8c2hvdmVsfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60"};
        var p2 = new PhotoDAO(){IdObject = o1.IdObject, Path = "https://images.unsplash.com/photo-1596277922657-f80257171aec?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8NHx8c2hvdmVsfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60"};
        var p3 = new PhotoDAO(){IdObject = o1.IdObject, Path = "https://images.unsplash.com/photo-1633859118812-a5abc2ff4de4?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTR8fHNob3ZlbHxlbnwwfHwwfHw%3D&auto=format&fit=crop&w=600&q=60"};
        var p4 = new PhotoDAO(){IdObject = o4.IdObject, Path = "https://images.unsplash.com/photo-1585483266684-b2f19ff1ba1d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8cGlvY2hlfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60"};
        var p5 = new PhotoDAO(){IdObject = o5.IdObject, Path = "https://plus.unsplash.com/premium_photo-1661963687013-36b88a78062e?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MTl8fGNlbWVudCUyMG1peGVyfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=600&q=60"};
        var p6 = new PhotoDAO(){IdObject = o5.IdObject, Path = "https://plus.unsplash.com/premium_photo-1674927125657-e8782933d009?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8N3x8Y2VtZW50JTIwbWl4ZXJ8ZW58MHx8MHx8&auto=format&fit=crop&w=600&q=60"};
        modelBuilder.Entity<PhotoDAO>().HasData(new List<PhotoDAO>(){p1,p2,p3,p4,p5,p6});

        if (this.seedData != null)
        {
            // Cette fonction est fournie par l'injection de dépendance
            this.seedData(modelBuilder);
        }
    }
}