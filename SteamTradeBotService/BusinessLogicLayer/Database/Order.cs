using System.ComponentModel.DataAnnotations;

namespace SteamTradeBotService.BusinessLogicLayer.Database
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string EngItemName { get; set; }
        public string RusItemName { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
    }
}
