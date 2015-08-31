using System;
using System.Collections.Generic;

namespace ReactServerSide.Models
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Comments = new List<Comment>();
        }

        public IEnumerable<Comment> Comments { get; set; }
    }

    public class Author
    {
        public string Name { get; set; }
        public string GithubUsername { get; set; }
    }

    public class Comment
    {
        public Author Author { get; set; }
        public string Text { get; set; }
    }
}