﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entity
{
    public class Topic
    {
        [Key]
        public int TitleID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Like { get; set; }
        public DateTime AddedDate { get; set; }
        public string UserAddedID { get; set; }
        public int CategoryID { get; set; }
    }
}