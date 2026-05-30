namespace AuthAdminCrud.MVC.Models
{
    public class BasketItem : BaseEntity
    {
        protected BasketItem()
        {

        }
        public BasketItem(Guid productId, int quantity, Guid userId)
        {
            SetProductId(productId);
            SetCount(quantity);
            SetUserId(userId);
        }
        public Guid ProductId { get; private set; }
        public Guid UserId { get; private set; }
        public AppUser User { get; private set; }
        public Product Product { get; private set; }
        public int Count { get; private set; }
        private void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentException("User ID cannot be empty.");
            }
            UserId = userId;
        }
        private void SetProductId(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty.");
            }
            ProductId = productId;
        }
        private void SetCount(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Count must be greater than zero.");
            }
            Count = count;
        }
        public void UpdateCount(int count)
        {
            SetCount(count);
        }
    }
}
