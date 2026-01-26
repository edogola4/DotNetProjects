using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

namespace TaskManagerApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public DbSet<TaskAttachment> TaskAttachments { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.PasswordHash).IsRequired();
        });
        
        // TaskItem configuration
        modelBuilder.Entity<TaskItem>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasIndex(t => t.UserId);
            entity.HasIndex(t => t.DueDate);
            entity.HasIndex(t => t.Status);
            entity.HasIndex(t => t.IsDeleted);
            entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
            entity.Property(t => t.Description).HasMaxLength(1000);
            entity.Property(t => t.Status).HasConversion<int>();
            
            entity.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
                
            entity.HasOne(t => t.CompletedByUser)
                .WithMany()
                .HasForeignKey(t => t.CompletedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        
        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => new { c.UserId, c.Name }).IsUnique();
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            
            entity.HasOne(c => c.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // Tag configuration
        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.HasIndex(t => t.Name).IsUnique();
            entity.Property(t => t.Name).IsRequired().HasMaxLength(50);
        });
        
        // Many-to-many: TaskItem <-> Tag
        modelBuilder.Entity<TaskItem>()
            .HasMany(t => t.Tags)
            .WithMany(tag => tag.Tasks)
            .UsingEntity(j => j.ToTable("TaskTags"));
            
        // TaskComment configuration
        modelBuilder.Entity<TaskComment>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.HasIndex(c => c.TaskId);
            entity.HasIndex(c => c.UserId);
            entity.HasIndex(c => c.IsDeleted);
            entity.Property(c => c.Content).IsRequired().HasMaxLength(1000);
            
            entity.HasOne(c => c.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // TaskAttachment configuration
        modelBuilder.Entity<TaskAttachment>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.HasIndex(a => a.TaskId);
            entity.HasIndex(a => a.UserId);
            entity.Property(a => a.FileName).IsRequired().HasMaxLength(255);
            entity.Property(a => a.FilePath).IsRequired().HasMaxLength(500);
            entity.Property(a => a.ContentType).IsRequired().HasMaxLength(100);
            
            entity.HasOne(a => a.Task)
                .WithMany(t => t.Attachments)
                .HasForeignKey(a => a.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(a => a.User)
                .WithMany(u => u.Attachments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        // UserProfile configuration
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.FirstName).HasMaxLength(50);
            entity.Property(p => p.LastName).HasMaxLength(50);
            entity.Property(p => p.PhoneNumber).HasMaxLength(20);
            entity.Property(p => p.TimeZone).IsRequired().HasMaxLength(50);
            entity.Property(p => p.DateFormat).IsRequired().HasMaxLength(20);
            entity.Property(p => p.AvatarUrl).HasMaxLength(500);
            entity.Property(p => p.Bio).HasMaxLength(500);
            
            entity.HasOne(p => p.User)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
