using WebPortalEverthing.Models.ServiceCenter;

namespace WebPortalEverything.Models.ServiceCenter
{
    public class CreateModelViewModel
    {
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public int TypeId { get; set; }

        public List<ProducerViewModel> Producers { get; set; } = new List<ProducerViewModel>();
        public List<TypeViewModel> Types { get; set; } = new List<TypeViewModel>();
    }
}
