﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.DataProcessor.ImportDto
{
    public class ImportHallDto
    {
        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }

        public bool Is4Dx { get; set; }

        public bool Is3D { get; set; }

        [Range(1, 2147483647)]
        public int Seats { get; set; }
    }
}
