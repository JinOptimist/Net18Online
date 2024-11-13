namespace Everything.Data.Interface.Models
{
    public interface IAnimeReviewData : IBaseModel
    {
        public string Name { get; set; }
        public string Review { get; set; }
    }
}
