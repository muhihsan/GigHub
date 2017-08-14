﻿using GigHub.Data;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Repositories
{
    public class GenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}
