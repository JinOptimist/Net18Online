﻿namespace WebPortalEverthing.Models.GameStore
{
    public class GameViewModel
    {
        public string UserName { get; set; }
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string NameGame {  get; set; }

        public string ImageSrc { get; set; }

        public List<string> Tags { get; set; }
        
    }
}
