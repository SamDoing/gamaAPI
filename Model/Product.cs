﻿using System.ComponentModel.DataAnnotations;

namespace Gama_API.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }
    }
}
