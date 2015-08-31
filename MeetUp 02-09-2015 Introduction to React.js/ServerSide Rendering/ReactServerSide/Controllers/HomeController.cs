using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReactServerSide.Models;

namespace ReactServerSide.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel {
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Text = "This is the first comment",
                        Author = new Author
                        {
                            Name = "Daniel Lo Nigro",
                            GithubUsername = "Daniel15"
                        }
                    },
                    new Comment
                    {
                        Text = "This is the second comment",
                        Author = new Author
                        {
                            Name = "Christopher Chedeau",
                            GithubUsername = "vjeux"
                        }
                    },
                    new Comment
                    {
                        Text = "This is the third comment",
                        Author = new Author
                        {
                            Name = "Christoph Pojer",
                            GithubUsername = "cpojer"
                        }
                    }
                }
            };
            return View(model);
        }
    }
}