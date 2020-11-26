using EmployeeMessenger.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMessenger.Infrastructure.EntityFramework
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<WorkspaceRole> WorkspaceRoles { get; set; }
        public DbSet<WorkspaceUser> WorkspaceUsers { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<PermissionToChannel> ParmissionsToChannels { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<WorkspaceUser>()
                .HasKey(wc => new { wc.WorkspaceId, wc.UserId });

            builder
                .Entity<WorkspaceUser>()
                .HasIndex(wu => new { wu.WorkspaceId, wu.UserId })
                .IsUnique(true);

            builder
                .Entity<Workspace>()
                .HasMany<WorkspaceUser>(w => w.WorkspaceUsers)
                .WithOne(wu => wu.Workspace)
                .HasForeignKey(wu => wu.WorkspaceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<WorkspaceUser>()
                .HasOne(wu => wu.Workspace)
                .WithMany(w => w.WorkspaceUsers)
                .HasForeignKey(wu => wu.WorkspaceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<WorkspaceUser>()
                .HasOne(wu => wu.User)
                .WithMany(u => u.WorkspaceUsers)
                .HasForeignKey(wu => wu.UserId);

            builder
                .Entity<Channel>()
                .HasMany(c => c.ParmissionsToChannels)
                .WithOne(p => p.Channel);

            builder
                .Entity<Channel>()
                .HasOne(c => c.Workspace)
                .WithMany(w => w.WorkspaceChannels)
                .HasForeignKey(c => c.WorkspaceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Channel>()
                .HasOne(c => c.ChannelType)
                .WithMany()
                .HasForeignKey(c => c.ChannelTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<PermissionToChannel>()
                .HasIndex(p => new { p.ChannelId, p.UserId, p.WorkspaceId })
                .IsUnique(true);

            builder.Entity<PermissionToChannel>()
                .HasOne(p => p.Channel)
                .WithMany(c => c.ParmissionsToChannels)
                .HasForeignKey(p => p.ChannelId);

            builder.InitializeData();

            base.OnModelCreating(builder);
        }
    }
}
