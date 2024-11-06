namespace Everything.Data.Interface.Models
{
    public interface IAnimeDescriptionData : IBaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
