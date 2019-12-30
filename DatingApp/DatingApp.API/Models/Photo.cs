﻿using System;
namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAddede { get; set; }
        public bool IsMain { get; set; }
    }
}