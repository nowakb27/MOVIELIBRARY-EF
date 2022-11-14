using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace EFTutorial
{
    public class Program
    {
        static void Main(string[] args)
        {
            var userChoice = "";
            do
            {
                Console.WriteLine("SELECT AN OPTION: ");
                Console.WriteLine("(1) DISPLAY ALL BLOGS");
                Console.WriteLine("(2) ADD A BLOG");
                Console.WriteLine("(3) DISPLAY POSTS");
                Console.WriteLine("(4) CREATE A POST");
                Console.WriteLine("(ENTER 'q' TO QUIT)");

                userChoice = Console.ReadLine();


                if (userChoice == "1")
                {
                    Console.WriteLine("LIST OF BLOGS: ");
                    using (var context = new BlogContext())
                    {
                        var blogs = context.Blogs;
                        foreach (var blog in blogs)
                        {
                            Console.WriteLine($"Blog Id: {blog.BlogId} {blog.Name}");
                        }
                    }
                }

                if (userChoice == "2")
                {
                    Console.WriteLine("ENTER BLOG NAME: ");
                    var name = Console.ReadLine();

                    if (name == "")
                    {
                        Console.WriteLine("*NAME OF BLOG CANNOT BE LEFT BLANK*");
                    }
                    else
                    {
                        var blog = new Blog();
                        blog.Name = name;

                        using (var context = new BlogContext())
                        {
                            context.Blogs.Add(blog);
                            context.SaveChanges();
                        }
                    }
                }

                if (userChoice == "3")
                {
                    Console.WriteLine("WHICH BLOG WOULD YOU LIKE TO SEE POSTS FROM? ENTER THE BlogId: ");
                    using (var context = new BlogContext())
                    {
                        var blogs = context.Blogs;
                        foreach (var blog in blogs)
                        {
                            Console.WriteLine($"BLOG ID: {blog.BlogId} BLOG NAME: {blog.Name}");
                        }

                        var blogChoice = int.Parse(Console.ReadLine());

                        if (blogChoice.Equals("") || blogChoice > context.Blogs.Count())
                        {
                            Console.WriteLine("*INVALID CHOICE*");
                        }

                        else
                        {
                            var posts = context.Posts.Where(x => x.BlogId == blogChoice);
                            foreach (var post in posts)
                            {
                                Console.WriteLine($"BLOG NAME: {post.Blog.Name}, TITLE: {post.Title}, Content: {post.Content}");
                            }
                        }
                    }
                }

                if (userChoice == "4")
                {
                    Console.WriteLine("WHICH BLOG WOULD YOU LIKE TO POST TO? ENTER BlogId: ");
                    using (var context = new BlogContext())
                    {
                        var blogs = context.Blogs;
                        foreach (var blog in blogs)
                        {
                            Console.WriteLine($"BLOG ID: {blog.BlogId} BLOG NAME: {blog.Name}");
                        }

                        var blogChoice = int.Parse(Console.ReadLine());

                        if (blogChoice.Equals("") || blogChoice > context.Blogs.Count())
                        {
                            Console.WriteLine("INVALID ANSWER");
                        }

                        else
                        {
                            Console.WriteLine("ENTER POST TITLE: ");
                            var title = Console.ReadLine();
                            Console.WriteLine("ENTER POST CONTENT: ");
                            var content = Console.ReadLine();

                            var post = new Post();
                            post.Title = title;
                            post.Content = content;
                            post.BlogId = blogChoice; 

                            context.Posts.Add(post);
                            context.SaveChanges();
                        }
                    }
                }
            } while (userChoice.ToLower() != "q");

        }
    }
}