﻿using GigHub.Persistence.Data;
using GigHub.Core.Models;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
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
