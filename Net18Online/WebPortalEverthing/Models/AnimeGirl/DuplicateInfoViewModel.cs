namespace WebPortalEverthing.Models.AnimeGirl
{
    public class DuplicateInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageSrc { get; set; }
        public string UniqStatus { get; set; }
        public string DuplicateStatus { get; set; }
        public int? OriginId { get; set; }
        public string? OriginName { get; set; }
    }
}
