using Library_Final_Project.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
public class LibraryDbContext : IdentityDbContext<ApplicationUser>
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<CartBook> CartBooks { get; set; }
    public DbSet<DeliveryType> DeliveryTypes { get; set; }
    public DbSet<FavoriteBook> FavoriteBooks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderBook> OrderBooks { get; set; }
    public DbSet<OrderState> OrderStates { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<ReviewState> ReviewStates { get; set; }
    public DbSet<UserBook> UserBooks { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany().HasForeignKey(x => x.ParentCategoryId);
        builder.Entity<CartBook>().HasKey(x => new { x.BookId, x.UserId });
        builder.Entity<BookAuthor>().HasKey(x => new { x.BookId, x.AuthorId });
        builder.Entity<FavoriteBook>().HasKey(x => new { x.BookId, x.UserId });
        builder.Entity<OrderBook>().HasKey(x => new { x.BookId, x.OrderId });
        builder.Entity<UserBook>().HasKey(x => new { x.BookId, x.UserId });

        builder.Entity<Category>().HasData
        (
            new List<Category>
            {
                    new Category {Id = 1, Name = "Каталог"},
                    new Category {Id = 2, Name = "Комиксы", ParentCategoryId = 1},
                    new Category {Id = 3, Name = "Художественная литература", ParentCategoryId = 1},
                    new Category {Id = 4, Name = "Поэзия", ParentCategoryId = 3},
                    new Category {Id = 5, Name = "Романы", ParentCategoryId = 3}
            }
        );

        builder.Entity<OrderState>().HasData(new List<OrderState>
            {
                new OrderState {Id = 1, Name = nameof(Library_Final_Project.Common.Enum.OrderStates.New)},
                new OrderState {Id = 2, Name = nameof(Library_Final_Project.Common.Enum.OrderStates.Approved)},
                new OrderState {Id = 3, Name = nameof(Library_Final_Project.Common.Enum.OrderStates.Reject)},
                new OrderState {Id = 4, Name = nameof(Library_Final_Project.Common.Enum.OrderStates.Delivered)}
            });

        builder.Entity<ReviewState>().HasData(new List<ReviewState>
            {
                new ReviewState {Id = 1, Name = nameof(Library_Final_Project.Common.Enum.ReviewStates.New)},
                new ReviewState {Id = 2, Name = nameof(Library_Final_Project.Common.Enum.ReviewStates.Approved)},
                new ReviewState {Id = 3, Name = nameof(Library_Final_Project.Common.Enum.ReviewStates.Reject)},
                new ReviewState {Id = 4, Name = nameof(Library_Final_Project.Common.Enum.ReviewStates.Modified)}
            });

        builder.Entity<Author>(opt =>
        {
            opt.HasData(new List<Author>
                {
                    new Author { Id = 1, Name = "Вэнс Эшли"},
                    new Author { Id = 2, Name = "Айзексон Уолтер"},
                    new Author { Id = 3, Name = "Джорж Клейсон"},
                    new Author { Id = 4, Name = "Джон Кехо"},
                    new Author { Id = 5, Name = "Анна Т"}
                });
        });
        builder.Entity<DeliveryType>(opt =>
        {
            opt.HasData(new List<DeliveryType>
                {
                    new DeliveryType { Id = 1, Name = "По городу", Price = 5}
                });
        });

        builder.Entity<PaymentType>(opt =>
        {
            opt.HasData(new List<PaymentType>
                {
                    new PaymentType {Id = 1, Name = "Оплата при доставке"}
                });
        });

        builder.Entity<Book>(opt =>
        {
            opt.HasData(new List<Book>
                {
                    new Book { Id = 1, Available = true, Description = "«„Я похож на сумасшедшего?“ — спросил меня Илон Маск». О чем книга В книге «Илон Маск: Tesla, SpaceX и дорога в будущее» автор представляет независимый и разносторонний взгляд на жизнь и достижения самого яркого предпринимателя Кремниевой долины.", Language = "Русский", Percent = 0, Price = 124, Title = "Вэнс Эшли: Илон Маск. Tesla, SpaceX", AvailableCount = 100, HasPdf = false, ImagePath = "/img/ilon.jpg", PagesNumber = 390, PdfPath = null, ISBN = "148419606", PublishYear = 2019,  CategoryId= 1},
                    new Book { Id = 2, Available = true, Description = "Книга, которая изменит вашу жизнь! Самое известное исследование о подсознании от известного писателя и тренера личностного роста Джона Кехо! В подсознании каждого человека скрываются огромные резервы. И когда логика оказывается бессильной, именно подсознание поможет вам решать самые сложные повседневные проблемы.", Language = "Русский", Percent = 0, Price = 68, Title = "Джон Кехо: Подсознание может все", AvailableCount = 100, HasPdf = false, ImagePath = "/img/podsoznanie.jpg", PagesNumber = 174, PdfPath = null, ISBN = "17574992", PublishYear = 2019, CategoryId = 2},
                    new Book { Id = 3, Available = true, Description = "Автор этой книги уверен: чтобы исполнить все свои замыслы и желания, Вы прежде всего должны добиться успеха в денежных вопросах, используя принципы управления личными финансами, изложенные на ее страницах.", Language = "Русский", Percent = 0, Price = 64, Title = "Джорж Клейсон: Самый богатый человек в Вавилоне", AvailableCount = 50, HasPdf = false, ImagePath = "/img/samiybogatiy.png", PagesNumber = 190, PdfPath = null, ISBN = "29968802", PublishYear = 2020, CategoryId = 3},
                    new Book { Id = 4, Available = true, Description = "В основу книги Уолтера Айзексона Стив Джобс легли беседы с самим Стивом Джобсом, а также с его родственниками, друзьями, врагами, соперниками и коллегами. Джобс никак не контролировал автора. Он откровенно отвечал на все вопросы и ждал такой же честности от остальных.", Language = "Русский", Percent = 0, Price = 146, Title = "Айзексон Уолтер: Стив Джобс. Биография", AvailableCount = 606, HasPdf = false, ImagePath = "/img/stivjobs.jpg", PagesNumber = 100, PdfPath = null, ISBN = "240540450", PublishYear = 2017, CategoryId = 1},
                    new Book { Id = 5, Available = true, Description = "Если даже такая нищебродская тушка, как я сумела поправить свое финансовое положение, сможет кто угодно! - заявляет Джен Синсеро. И ей сложно не верить. До сорока с лишнем лет она жила в переделанном гараже, одевалась в секонд-хэнде и не могла себе позволить вылечить зубы.", Language = "Русский", Percent = 0, Price = 99, Title = "НЕ НОЙ.", AvailableCount = 60, HasPdf = false, ImagePath = "/img/nenoy.jpg", PagesNumber = 304, PdfPath = null, ISBN = "k9052", PublishYear = 2019, CategoryId = 2},
                    new Book { Id = 6, Available = true, Description = "Эта книга - ваш личный тренер. Каждый день она будет поднимать боевой дух и заряжать на успех. Ее автор, знаменитая Джен Синсеро, призывает не сбавлять обороты на пути к успеху и ежедневно накачивать мышцы крутости в Духовном тренажерном зале. Успех - это способ существования, постоянной адаптации и роста.", Language = "Русский", Percent = 0, Price = 99, Title = "Не тупи", AvailableCount = 30, HasPdf = false, ImagePath = "/img/netupi.jpg", PagesNumber = 306, PdfPath = null, ISBN = "k5043", PublishYear = 2019, CategoryId = 4}
                });
        });

        
        builder.Entity<BookAuthor>(opt =>
        {
            opt.HasData(new List<BookAuthor>
                {
                    new BookAuthor {AuthorId = 1, BookId = 1},
                    new BookAuthor {AuthorId = 2, BookId = 2},
                    new BookAuthor {AuthorId = 3, BookId = 3},
                    new BookAuthor {AuthorId = 4, BookId = 4},
                    new BookAuthor {AuthorId = 5, BookId = 5},
                    new BookAuthor {AuthorId = 1, BookId = 6}
                });
        });
    }
}
