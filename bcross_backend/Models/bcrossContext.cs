using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace bcross_backend.Models
{
    public partial class bcrossContext : DbContext
    {
        public bcrossContext()
        {
        }

        public bcrossContext(DbContextOptions<bcrossContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Bookrecord> Bookrecords { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=bcross;Username=postgres;Password=8df8ea0a");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp")
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Uuid)
                    .HasColumnName("uuid")
                    .HasDefaultValueSql("uuid_generate_v1()");

                entity.Property(e => e.Year)
                    .IsRequired()
                    .HasColumnName("year");
            });

            modelBuilder.Entity<Bookrecord>(entity =>
            {
                entity.ToTable("bookrecord");

                entity.HasIndex(e => e.Book, "idx_bookrecord__book");

                entity.HasIndex(e => e.Receiver, "idx_bookrecord__receiver");

                entity.HasIndex(e => e.Sender, "idx_bookrecord__sender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Book).HasColumnName("book");

                entity.Property(e => e.Commentary)
                    .IsRequired()
                    .HasColumnName("commentary");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location");

                entity.Property(e => e.Receiver).HasColumnName("receiver");

                entity.Property(e => e.Receivetime).HasColumnName("receivetime");

                entity.Property(e => e.Sender).HasColumnName("sender");

                entity.Property(e => e.Sendtime).HasColumnName("sendtime");

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Bookrecords)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("fk_bookrecord__book");

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.BookrecordReceiverNavigations)
                    .HasForeignKey(d => d.Receiver)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_bookrecord__receiver");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.BookrecordSenderNavigations)
                    .HasForeignKey(d => d.Sender)
                    .HasConstraintName("fk_bookrecord__sender");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasIndex(e => e.Receiver, "idx_message__receiver");

                entity.HasIndex(e => e.Sender, "idx_message__sender");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Receiver).HasColumnName("receiver");

                entity.Property(e => e.Sender).HasColumnName("sender");

                entity.Property(e => e.Textmessage).HasColumnName("textmessage");

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.MessageReceiverNavigations)
                    .HasForeignKey(d => d.Receiver)
                    .HasConstraintName("fk_message__receiver");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.MessageSenderNavigations)
                    .HasForeignKey(d => d.Sender)
                    .HasConstraintName("fk_message__sender");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname");

                entity.Property(e => e.Phone).HasColumnName("phone");

                entity.Property(e => e.Rating).HasColumnName("rating");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
