using Microsoft.AspNetCore.Identity;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Repositories.Interfaces;
using WebShop.Services.Interfaces;

namespace WebShop.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly UserManager<User> _userManager;
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        public CartService
            (
                UserManager<User> userManager,
                IProductRepository productRepository,
                ICartRepository cartRepository
            )
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task<bool> ClearAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync( userId );
            Cart cart = await _cartRepository.GetByUserAsync(user);

            cart.CartProducts.Clear();
            await _cartRepository.SaveAsync();

            if ( cart != null )
            {
                return false;
            }
            return true;
        }

        public async Task<CartR> GetAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            Cart cart = await _cartRepository.GetByUserAsync(user);

            CartR cartDto = MapToDto(cart);
            cartDto.TotalPrice = CalculateTotalPrice(cartDto);

            return cartDto;
        }

        public async Task<bool> UpdateAsync(CartW cartDto)
        {
            Cart cart = await MapFromDto(cartDto);
            foreach (var orderItem in cart.CartProducts)
            {
                if (_productRepository.CheckEnoughProduct(orderItem.Product, orderItem.Quantity) == false)
                {
                    throw new NotEnoughProductException($"Недостаточно товаров {orderItem.Product.Title}");
                }
            }
            return await _cartRepository.UpdateAsync( cart );
        }
        private async Task<Cart> MapFromDto(CartW cartDto)
        {
            Cart cart = new Cart
            {
                UserId = cartDto.UserId,    
                User = await _userManager.FindByIdAsync(cartDto.UserId),
            };
            foreach (var cartItem in cartDto.CartProducts)
            {
                cart.CartProducts.Add
                    (
                        new CartProduct
                        {
                            Cart = cart,
                            Product = await _productRepository.GetAsync(cartItem.Key),
                            Quantity =cartItem.Value
                        }
                    );
            }
            return cart;
        }
        private CartR MapToDto(Cart cart)
        {
            CartR cartDto = new CartR();
            cartDto.CartItems = cart.CartProducts
                .Select(c => new OrderItemR
                {
                    ProductId = c.Product.Id,
                    ProductTitle = c.Product.Title,
                    ImageUrl = c.Product.ImageUrl,
                    Price = c.Product.Price,
                    Quantity = c.Quantity
                }).ToList();
            return cartDto;
        }
        private decimal CalculateTotalPrice(CartR cartDto)
        {
            var totalPrice = 0m;
            totalPrice = cartDto.CartItems.Sum(item => item.Quantity * item.Price);
            return totalPrice;
        }
    }
}
