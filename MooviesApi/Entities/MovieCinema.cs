﻿using System;
namespace MoviesApi.Entities
{
	public class CinemaMovie
	{
		public int MovieId { get; set; }
		public Movie Movie { get; set; }
		public int CinemaId { get; set; }
		public Cinema Cinema { get; set; }
	}
}

