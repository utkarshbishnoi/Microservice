namespace InvertoryService.Models.Dto
{
    public class ResponseDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Size {  get; set; }
        public string Design { get; set; }
    }
}
