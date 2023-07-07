using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ChatApi.Models
{
    public partial class chatappContext : DbContext
    {
        public chatappContext()
        {
        }

        public chatappContext(DbContextOptions<chatappContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; } = null!;
        public virtual DbSet<UserInfo> UserInfos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PC0599\\MSSQL2019;Database=chatapp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");

                entity.Property(e => e.ChatId).HasColumnName("chatId");

                entity.Property(e => e.HashMessage).IsUnicode(false);

                entity.Property(e => e.MessageTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SaltMessage).IsUnicode(false);

                entity.Property(e => e.SenderId).HasColumnName("senderId");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.ChatReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .HasConstraintName("FK__Chat__ReceiverId__3C69FB99");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.ChatSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Chat__senderId__3B75D760");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserInfo__1788CC4CAAB1B27B");

                entity.ToTable("UserInfo");

                entity.Property(e => e.Username)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
