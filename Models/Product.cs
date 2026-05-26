namespace AuthAdminCrud.MVC.Models
{
    public class Product: BaseEntity
    {
        public Product()
        {
            
        }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get;private set; }
        public int Price { get;private set; }

        public void Update(string name, int price)
        {
            Name = name;
            Price = price;
        }
        
    }
}