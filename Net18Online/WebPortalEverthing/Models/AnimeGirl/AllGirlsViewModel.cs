﻿namespace WebPortalEverthing.Models.AnimeGirl
{
    public class AllGirlsViewModel
    {
        public PagginatorViewModel<GirlViewModel> Girls { get; set; }

        public List<MangaNameAndIdViewModel> Mangas { get; set; }
    }
}
