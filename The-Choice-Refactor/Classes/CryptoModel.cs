namespace The_Choice_Refactor.Classes
{
    public class CryptoModel
    {
        public int number { get; set; }
        public string? asset_id { get; set; }
        public string? name { get; set; }
        public double price { get; set; }
        public double volume_24h { get; set; }
        public double change_1h { get; set; }
        public string? color_change_1h { get; set; }
        public double change_24h { get; set; }
        public string? color_change_24h { get; set; }
        public double change_7d { get; set; }
        public string? color_change_7d { get; set; }
        public bool isFavorite { get; set; } = false; // describe is asset in favorites
    }
}
