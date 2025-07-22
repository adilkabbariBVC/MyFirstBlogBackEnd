using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyFirstBlog.Entities;
using MyFirstBlog.Helpers;
using MyFirstBlog.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

public class PostServiceTests
{
    private DataContext _context;
    private PostService _service;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>())
            .Build();

        _context = new DataContext(config, options);
        _context.Database.EnsureCreated();

        _service = new PostService(_context);
    }

    [Test]
    public void CreatePost_Should_Add_Post_To_Database()
    {
        // Arrange
        var title = "My First Post";
        var body = "This is the body.";

        // Act
        var result = _service.CreatePost(title, body);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(title, result.Title);
        Assert.AreEqual("my-first-post", result.Slug);
    }

    [Test]
    public void GetPost_Should_Return_Correct_Post_By_Slug()
    {
        // Arrange
        var post = _service.CreatePost("Test Post", "Test Body");

        // Act
        var result = _service.GetPost(post.Slug);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(post.Title, result.Title);
    }

    [Test]
    public void GetPosts_Should_Return_All_Posts()
    {
        // Arrange
        _service.CreatePost("Post 1", "Body 1");
        _service.CreatePost("Post 2", "Body 2");

        // Act
        var posts = _service.GetPosts().ToList();

        // Assert
        Assert.AreEqual(2, posts.Count);
    }

    [Test]
    public void CreatePost_Should_Throw_If_Title_Is_Blank()
    {
        // Arrange & Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
            _service.CreatePost(" ", "Body"));

        Assert.AreEqual("Title cannot be blank", ex.Message);
    }
}











//using NUnit.Framework;
//using MyFirstBlog.Services;
//using MyFirstBlog.Entities;
//using MyFirstBlog.Helpers;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;


//namespace MyFirstBlogTests;

//public class PostServiceTests
//{
//    private IPostService _service;
//    private DataContext _context;

//    [SetUp]
//    public void Setup()
//    {
//        var options = new DbContextOptionsBuilder<DataContext>()
//            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//            .Options;

//        var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
//        _context = new DataContext(configuration);
//        _context.Database.EnsureCreated();

//        _service = new PostService(_context);
//    }

//    [Test]
//    public void CreatePost_WithTitleAndBody_ReturnsPost()
//    {
//        var result = _service.CreatePost("Hello World", "Test Body");

//        Assert.IsNotNull(result);
//        Assert.AreEqual("Hello World", result.Title);
//        Assert.AreEqual("hello-world", result.Slug);
//    }

//    [Test]
//    public void CreatePost_WithEmptyTitle_ThrowsError()
//    {
//        var ex = Assert.Throws<ArgumentException>(() =>
//        {
//            _service.CreatePost("", "Test Body");
//        });

//        Assert.That(ex.Message, Is.EqualTo("Title cannot be blank"));
//    }
//}












//using NUnit.Framework;

//namespace MyFirstBlogTests
//{
//    public class Tests
//    {
//        [SetUp]
//        public void Setup()
//        {
//        }

//        [Test]
//        public void Test1()
//        {
//            Assert.Pass();
//        }
//    }
//}
