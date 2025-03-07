﻿using Microsoft.AspNetCore.Mvc.Rendering;
using WebPortalEverthing.Localizations;
using WebPortalEverthing.Models.CustomValidationAttrubites;

namespace WebPortalEverthing.Models.AnimeGirl
{
    public class GirlCreationViewModel
    {
        [UniqGirlName]
        public string Name { get; set; }

        [IsUrl(
            ErrorMessageResourceType = typeof(Girl), 
            ErrorMessageResourceName = nameof(Girl.ValidationMessage_IsNotUrl))]
        public string Url { get; set; }

        [IsCorrectHeight(110, 270, HeightOption.Sm)]
        public int Height { get; set; }

        [Weight]
        public int Weight { get; set; }

        public int MangaId { get; set; }
        public List<SelectListItem>? Mangas { get; set; }
    }
}
