namespace RedisCacheWebPoc.Controllers
{
    public class CardModel
    {
        public string ExpirationDate { get; set; }
        public string Holder { get; set; }
        public string Number { get; set; }
        public string SecurityCode { get; set; }
        public object Brand { get; set; }
    }
}