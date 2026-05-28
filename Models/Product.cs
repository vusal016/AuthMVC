namespace AuthAdminCrud.MVC.Models
{
    public class Product: BaseEntity
    {
        protected Product()
        {
            
        }

        public Product(string name, int price, string imageUrl, string buttonText)
        {
            SetName(name);
            SetPrice(price);
            SetImageUrl(imageUrl);
            SetButtonText(buttonText);
        }

        public string Name { get;private set; }
        public int Price { get;private set; }
        public string ImageUrl { get;private set; }
        public string ButtonText { get;private set; }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }
            Name = name;
        }
        private void SetImageUrl(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("Image URL cannot be null or empty.");
            }
            ImageUrl = imageUrl;
        }
        private void SetButtonText(string buttonText)
        {
            if (string.IsNullOrEmpty(buttonText))
            {
                throw new ArgumentException("Button text cannot be null or empty.");
            }
            ButtonText = buttonText;
        }
        private void SetPrice(int price)
        {
            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }
            Price = price;
        }
        public void Update(string name, int price)
        {
            SetName(name);
            SetPrice(price);
        }
        
    }
}