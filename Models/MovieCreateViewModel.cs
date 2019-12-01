using System;
using Microsoft.AspNetCore.Http;

namespace usermovieApp.Models

{
    public class MovieCreateViewModel
    {
        public Movie Movie {get; set;}

        public IFormFile ImageFile {get; set;}
    }
}
