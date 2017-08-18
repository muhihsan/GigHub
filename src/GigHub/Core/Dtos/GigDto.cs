﻿using System;

namespace GigHub.Core.Dtos
{
    public class GigDto
    {
        public int Id { get; private set; }
        public bool IsCancelled { get; private set; }
        public UserDto Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDto Genre { get; set; }
    }
}
