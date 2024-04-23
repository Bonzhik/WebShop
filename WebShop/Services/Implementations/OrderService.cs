using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public OrderService
            (
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IStatusRepository statusRepository,
            UserManager<User> userManager,
            IMapper mapper
            )
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _statusRepository = statusRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(OrderW orderDto)
        {
            Order order = await MapFromDto(orderDto);
            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;
            foreach (var orderItem in order.OrderProducts)
            {
                if (_productRepository.CheckEnoughProduct(orderItem.Product, orderItem.Quantity) == false)
                {
                    throw new NotEnoughProductException($"Недостаточно товаров {orderItem.Product.Title}");
                }
            }
            order.TotalPrice = CalculateTotalPrice(order);

            return await _orderRepository.AddAsync(order);
        }

        public async Task<bool> DeleteAsync(int orderId)
        {
            Order order = await _orderRepository.GetAsync(orderId);

            if (order == null)
            {
                throw new NotFoundException($"Отзыв {orderId} не найден");
            }

            return await _orderRepository.DeleteAsync(order);
        }

        public async Task<List<OrderR>> GetAllAsync()
        {
            List<Order> orders = await _orderRepository.GetAllAsync();

            List<OrderR> orderDtos = new List<OrderR>();
            foreach (var order in orders)
            {
                orderDtos.Add(MapToDto(order));
            }

            return orderDtos;
        }

        public async Task<OrderR> GetAsync(int id)
        {
            Order order = await _orderRepository.GetAsync(id);

            return MapToDto(order);
        }

        public async Task<List<OrderR>> GetByUserAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new NotFoundException($"Пользователь {userId} не найден");

            List<Order> orders = await _orderRepository.GetByUserAsync(user);

            List<OrderR> orderDtos = new List<OrderR>();
            foreach (var order in orders)
            {
                orderDtos.Add(MapToDto(order));
            }

            return orderDtos;
        }

        public async Task<bool> UpdateAsync(OrderW orderDto)
        {
            Order order = await MapFromDto(orderDto);
            order.UpdatedAt = DateTime.UtcNow;
            foreach (var orderItem in order.OrderProducts)
            {
                if (_productRepository.CheckEnoughProduct(orderItem.Product, orderItem.Quantity) == false)
                {
                    throw new NotEnoughProductException($"Недостаточно товаров {orderItem.Product.Title}");
                }
            }
            return await _orderRepository.UpdateAsync(order);
        }
        private async Task<Order> MapFromDto(OrderW orderDto)
        {
            Order order = new Order
            {
                Id = orderDto.Id,
                Address = orderDto.Address,
                Status = await _statusRepository.GetAsync(orderDto.StatusId),
                User = await _userManager.FindByIdAsync(orderDto.UserId)
            };
            foreach (var orderItem in orderDto.OrderProducts)
            {
                order.OrderProducts.Add
                    (
                        new OrderProduct
                        {
                            Order = order,
                            Product = await _productRepository.GetAsync(orderItem.Key),
                            Quantity = orderItem.Value
                        }
                    );
            }
            return order;
        }
        private OrderR MapToDto(Order order)
        {
            OrderR orderDto = new OrderR
            {
                Id = order.Id,
                Address = order.Address,
                Status = order.Status.Title,
                UserId = order.User.Id,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                TotalPrice = order.TotalPrice,
            };
            foreach (var orderItem in order.OrderProducts)
            {
                orderDto.OrderItems.Add
                    (
                        new OrderItemR
                        {
                            ProductId = orderItem.Product.Id,
                            ProductTitle = orderItem.Product.Title,
                            ImageUrl = orderItem.Product.ImageUrl,
                            Price = orderItem.Product.Price,
                            Quantity = orderItem.Quantity,
                        }
                    );
            }
            return orderDto;
        }
        private decimal CalculateTotalPrice(Order order)
        {
            var totalPrice = 0m;
            totalPrice = order.OrderProducts.Sum(item => item.Quantity * item.Product.Price);
            return totalPrice;
        }
    }
}
