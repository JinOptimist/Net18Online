using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebPortalEverthing.Models.Ecology;

public class PostViewModel
{
    public List<EcologyViewModel> Ecologies { get; set; }
    
    public List<PostCreationViewModel> Posts { get; set; }
}