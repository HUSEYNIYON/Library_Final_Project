using Library_Final_Project.DTOs;
using Library_Final_Project.DTOs.Book;
using Library_Final_Project.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services
{
    public class OrderService
    {
        private readonly LibraryDbContext context;

        public OrderService(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task<CreateOrderViewModel> GetOrderBooksAsync(string userId)
        {
            var books = await context.CartBooks.Where(x => x.UserId == userId).Include(x => x.Book).Select(x => new OrderBookViewModel { Count = x.Count, Name = x.Book.Title, Price = x.Book.Price }).ToListAsync();

            return new CreateOrderViewModel
            {
                Books = books,
                DeliveryTypes = await context.DeliveryTypes.ToDictionaryAsync(x => x.Id, x => (x.Name + " - " + x.Price)),
                PaymentTypes = await context.PaymentTypes.ToDictionaryAsync(x => x.Id, x => x.Name)
            };
        }

        public async Task CreateAsync(CreateOrderViewModel model, string id)
        {
            var cartBooks = await context.CartBooks.Where(x=>x.UserId == id).ToListAsync();

            var orderBooks = new List<OrderBook>();

            foreach(var book in cartBooks)
            {
                orderBooks.Add(new OrderBook { BookId = book.BookId, Count = book.Count });
            }

            var order = new Order
            {
                Address = model.Address,
                Comment = model.Comment,
                CreateAt = DateTime.Now,
                DeliveryTypeId = model.DeliveryTypeId,
                OrderStateId = 1,
                PaymentTypeId = model.PaymentTypeId,
                UserId = id,
                OrderBooks = orderBooks
            };

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

        }
    }
}
