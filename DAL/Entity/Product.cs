﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }        
        public string Description { get; set; }
        public int Stock { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
