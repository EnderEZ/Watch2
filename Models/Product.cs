namespace aspcore_watchshop.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CateID { get; set; }
        public int WireID { get; set; }
        public string Image { get; set; }
        public int SaleCount { get; set; }
        public decimal Discount { get; set; }
    }
}