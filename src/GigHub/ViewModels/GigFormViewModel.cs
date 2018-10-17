using GigHub.Core.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using GigHub.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<GigsController, IActionResult>> create = (c => c.Create(this));
                Expression<Func<GigsController, IActionResult>> update = (c => c.Update(this));

                var action = Id == 0 ? create : update;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }
        
        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        } 
    }
}
