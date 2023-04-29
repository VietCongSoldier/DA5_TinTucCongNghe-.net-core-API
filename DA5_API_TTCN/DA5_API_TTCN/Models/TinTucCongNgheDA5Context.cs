using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DA5_API_TTCN.Models
{
    public partial class TinTucCongNgheDA5Context : DbContext
    {
        public TinTucCongNgheDA5Context()
        {
        }

        public TinTucCongNgheDA5Context(DbContextOptions<TinTucCongNgheDA5Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Categorynews> Categorynews { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Statistic> Statistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=VIET-PC\\SQLEXPRESS;Database=TinTucCongNgheDA5;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Accountid).HasColumnName("accountid");

                entity.Property(e => e.Decentralization)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("decentralization");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Memberid).HasColumnName("memberid");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Registrationdate)
                    .HasColumnType("date")
                    .HasColumnName("registrationdate");

                entity.Property(e => e.Statusmem)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("statusmem");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Categoryname)
                    .HasMaxLength(500)
                    .HasColumnName("categoryname");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Sort).HasColumnName("sort");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.Property(e => e.Visiblemenu).HasColumnName("visiblemenu");
            });

            modelBuilder.Entity<Categorynews>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("categorynews");

                entity.Property(e => e.CategorynewsId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("categorynewsID");

                entity.Property(e => e.Categorynewsname)
                    .HasMaxLength(500)
                    .HasColumnName("categorynewsname");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Linkngoai)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("linkngoai");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(500)
                    .HasColumnName("noidung");

                entity.Property(e => e.Sapxep).HasColumnName("sapxep");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.Property(e => e.Visiblemenu).HasColumnName("visiblemenu");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.Feedbackid).HasColumnName("feedbackid");

                entity.Property(e => e.Contents)
                    .HasMaxLength(100)
                    .HasColumnName("contents");

                entity.Property(e => e.Datecomment)
                    .HasColumnType("date")
                    .HasColumnName("datecomment");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Img)
                    .HasMaxLength(100)
                    .HasColumnName("img");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Namereader)
                    .HasMaxLength(50)
                    .HasColumnName("namereader");

                entity.Property(e => e.Newsid).HasColumnName("newsid");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Imageid).HasColumnName("imageid");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .HasColumnName("description");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .HasColumnName("link");

                entity.Property(e => e.Newsid).HasColumnName("newsid");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.Memberid).HasColumnName("memberid");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(3)
                    .HasColumnName("gender");

                entity.Property(e => e.Img)
                    .HasMaxLength(100)
                    .HasColumnName("img");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phonenumber");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("news");

                entity.Property(e => e.Newsid).HasColumnName("newsid");

                entity.Property(e => e.Authorid).HasColumnName("authorid");

                entity.Property(e => e.Categoryid).HasColumnName("categoryid");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .HasColumnName("image");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Keyword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("keyword");

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("link");

                entity.Property(e => e.Numread).HasColumnName("numread");

                entity.Property(e => e.Posttime)
                    .HasColumnType("datetime")
                    .HasColumnName("posttime");

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Statistic>(entity =>
            {
                entity.ToTable("statistic");

                entity.Property(e => e.Statisticid).HasColumnName("statisticid");

                entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");

                entity.Property(e => e.Postmostread)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("postmostread");

                entity.Property(e => e.Posttimenews)
                    .HasColumnType("datetime")
                    .HasColumnName("posttimenews");

                entity.Property(e => e.Visitnumer).HasColumnName("visitnumer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
