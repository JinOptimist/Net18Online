using System.Collections;

namespace WebPortalEverthing.Models.Ecology;

public class PostViewModel
{
    public List<EcologyViewModel> Ecologies { get; set; }
    
    public List<PostCreationViewModel> Posts { get; set; }
}