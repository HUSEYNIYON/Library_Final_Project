using Library_Final_Project.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Library_Final_Project.Context
{
    public class LibraryDbContext : IdentityDbContext<ApplicationUser>
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books{ get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookDeliveryType> BookDeliveryTypes { get; set; }
        public DbSet<BookPaymentType> BookPaymentTypes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartBook> CartBooks{ get; set; }
        public DbSet<DeliveryType> DeliveryTypes{ get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<FavoriteBook> FavoriteBooks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBook> OrderBooks { get; set; }
        public DbSet<OrderState> OrderStates{ get; set; }
        public DbSet<PaymentType> PaymentTypes{ get; set; }
        public DbSet<Review> Reviews{ get; set; }
        public DbSet<ReviewState> ReviewStates{ get; set; }
        public DbSet<UserBook> UserBooks{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany().HasForeignKey(x => x.ParentCategoryId);
            builder.Entity<Book>().HasOne(x => x.Discount).WithOne().HasForeignKey<Book>(x => x.DiscountId);
            builder.Entity<BookDeliveryType>().HasKey(x => new { x.BookId, x.DeliveryTypeId });
            builder.Entity<BookPaymentType>().HasKey(x => new { x.BookId, x.PaymentTypeId });
            builder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId });
            builder.Entity<FavoriteBook>().HasKey(x => new { x.BookId, x.UserId });
            builder.Entity<OrderBook>().HasKey(x => new { x.BookId, x.OrderId });
            builder.Entity<UserBook>().HasKey(x => new { x.BookId, x.UserId });

            builder.Entity<Category>().HasData
            (
                new List<Category>
                {
                    new Category { Id = 1, Name = "Каталог"},
                    new Category { Id = 2, Name = "Коммиксы", ParentCategoryId = 1},
                    new Category { Id = 3, Name = "Художественная литература", ParentCategoryId = 1},
                    new Category { Id = 4, Name = "Поэзия", ParentCategoryId =3},
                    new Category { Id = 5, Name = "Романы", ParentCategoryId =3}
                }
            );

            builder.Entity<OrderState>().HasData
            (
                new List<OrderState>
                {
                    new OrderState { Id = 1, Name = nameof(Common.Enum.OrderStates.New)},
                    new OrderState { Id = 2, Name = nameof(Common.Enum.OrderStates.Reject)},
                    new OrderState { Id = 3, Name = nameof(Common.Enum.OrderStates.Approved)},
                    new OrderState { Id = 4, Name = nameof(Common.Enum.OrderStates.Delivered)}
                }   
            );

            builder.Entity<ReviewState>().HasData
            (
                new List<ReviewState>
                {
                    new ReviewState { Id = 1, Name = nameof(Common.Enum.ReviewStates.New)},
                    new ReviewState { Id = 2, Name = nameof(Common.Enum.ReviewStates.Reject)},
                    new ReviewState { Id = 3, Name = nameof(Common.Enum.ReviewStates.Approved)},
                    new ReviewState { Id = 4, Name = nameof(Common.Enum.ReviewStates.Modified)}
                }
            );
        }
    }
}
