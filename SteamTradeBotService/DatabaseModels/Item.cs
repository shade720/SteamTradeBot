using System.ComponentModel.DataAnnotations;

namespace SteamTradeBotService.DatabaseModels
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
    }
}
