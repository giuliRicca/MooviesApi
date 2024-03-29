﻿using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Entities
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public Point Location { get; set; }
        public List<CinemaMovie> CinemasMovies { get; set; }
        //public CinemaOffer CinemaOffer { get; set; }
    }
}
