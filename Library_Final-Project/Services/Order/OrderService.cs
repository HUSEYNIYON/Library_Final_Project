using Library_Final_Project.DTOs;
using Library_Final_Project.DTOs.Book;
using Library_Final_Project.DTOs.Order;
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
        private readonly LibraryDbContext _context;

        public OrderService(LibraryDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ApproveAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task ApproveAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            order.OrderStateId = 2;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// RejectAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RejectAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            order.OrderStateId = 3;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>List of all orders</returns>
        public async Task<GetOrdersViewModel> GetAll()
        {
            return new GetOrdersViewModel
            {
                Orders = await _context.Orders.Select(x => new OrderViewModel
                {
                    Id = x.Id,
                    Comment = x.Comment,
                    Address = x.Address,
                    CreatedAt = x.CreateAt,
                    UserId = x.UserId,
                    StateId = x.OrderStateId
                }).ToListAsync(),
                OrderStates = await _context.OrderStates.ToDictionaryAsync(x => x.Id, x => x.Name)
            };
        }

        /// <summary>
        /// GetOrderBooksAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CreateOrderViewModel> GetOrderBooksAsync(string userId)
        {
            var books = await _context.CartBooks.Where(x => x.UserId == userId).Include(x => x.Book).Select(x => new OrderBookViewModel { Count = x.Count, Name = x.Book.Title, Price = x.Book.Price }).ToListAsync();

            return new CreateOrderViewModel
            {
                Books = books,
                DeliveryTypes = await _context.DeliveryTypes.ToDictionaryAsync(x => x.Id, x => (x.Name + " - " + x.Price)),
                PaymentTypes = await _context.PaymentTypes.ToDictionaryAsync(x => x.Id, x => x.Name)
            };
        }

        /// <summary>
        /// CreateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task CreateAsync(CreateOrderViewModel model, string id)
        {
            var cartBooks = await _context.CartBooks.Where(x=>x.UserId == id).ToListAsync();

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

            await _context.Orders.AddAsync(order);
             _context.CartBooks.RemoveRange(cartBooks);
            await _context.SaveChangesAsync();
        }
    }
}
