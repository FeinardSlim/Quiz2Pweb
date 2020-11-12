using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz2Pweb.Models
{
    public class Article
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Body { get; set; }
    }

    public class ArticleDBContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }
    }
}