namespace WebPortalEverything.Models.ServiceCenter
{
    public class ProducerShortInfoViewModel
    {
        public int Id { get; set; }
        public string ProducerName { get; set; }
        public List<ModelShortInfoViewModel> Models { get; set; } = new(); 
    }
}
