namespace WebPortalEverything.Models.ServiceCenter
{
    public class IndexProducerViewModel
    {
        public List<ProducerShortInfoViewModel> Producers { get; set; } = new();
        public List<ModelShortInfoViewModel> Models { get; set; } = new();
    }
}
