namespace WebPortalEverthing.Models.GameStore
{
    public class ShopViewModel
    {
        public int Id { get; set; }
        public string NameGame { get; set; }
        public string ImageSrc { get; set; }
        public int Cost { get; set; }
        public string? Studios { get; set; }

        public bool IsLiked { get; set; }
        public bool IsDisliked { get; set; }

        public int LikeCount { get; set; }  
        public int DislikeCount { get; set; }
    }
}
