using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Models.Admin;

namespace WebPortalEverthing.Models.LoadTesting.Admin
{
    public class LoadAdminViewModel
    {
        public List<LoadUserVewModel> LoadUsers { get; set; }

        public List<SelectListItem> Roles { get; set; }
    }
}
