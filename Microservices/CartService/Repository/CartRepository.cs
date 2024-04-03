using CartService.Models;

namespace CartService.Repository
{
    public class CartRepository
    {
        private Dictionary<int, List<CartItem>> _userCarts = new Dictionary<int, List<CartItem>>();

        public List<CartItem> GetUserCart(int userId)
        {
            if (_userCarts.TryGetValue(userId, out var cartItems))
            {
                return cartItems;
            }
            return new List<CartItem>();
        }

        public void AddToCart(int userId, CartItem cartItem)
        {
            if (!_userCarts.ContainsKey(userId))
            {
                _userCarts[userId] = new List<CartItem>();
            }
            _userCarts[userId].Add(cartItem);
        }

        public void Checkout(int userId)
        {
            if (_userCarts.ContainsKey(userId))
            {
                _userCarts[userId].Clear();
            }
        }
    }
}
