namespace Everything.Data.Models.SqlRawModels
{
    public class AnimeWithShortestReview
    {
        public int AnimeId { get; set; }
        public string AnimeName { get; set; }
        public string ImageSrc { get; set; }
        public int? ShortestReviewLength { get; set; }
    }
}
