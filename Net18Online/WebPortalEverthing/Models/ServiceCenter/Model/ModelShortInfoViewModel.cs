namespace WebPortalEverything.Models.ServiceCenter
{
    public class ModelShortInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }          
        public int ProducerId { get; set; }     

        public string ProducerName { get; set; } // Optional display name of producer
        public string TypeName { get; set; }     // Optional display name of type
    }
}
