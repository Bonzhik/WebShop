using System.Runtime.CompilerServices;
using WebShop.Models;

namespace WebShop.Data
{
    public static class SeedData
    {
        public static void Init(ApplicationContext db)
        {
            if (!db.Attributes.Any())
            {
                var attributes = new List<Models.Attribute>
                {
                    new Models.Attribute {Title = "Размер"},
                    new Models.Attribute {Title = "Цвет"},
                    new Models.Attribute {Title = "Экран"},
                    new Models.Attribute {Title = "Процессор"},
                    new Models.Attribute {Title = "Оперативная память"},
                    new Models.Attribute {Title = "Графический ускоритель"}
                };
                db.Attributes.AddRange(attributes);
                db.SaveChanges();

            }
            if (!db.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category{Title = "Компьютер", Attributes = db.Attributes.ToList() },
                    new Category{Title = "Ноутбук", Attributes = db.Attributes.ToList()}
                };
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            if (!db.Subcategories.Any())
            {
                var subcategories = new List<Subcategory>
                {
                    new Subcategory{Title = "Игровой", Category = db.Categories.FirstOrDefault(c => c.Title == "Компьютер")},
                    new Subcategory{Title = "Обычный", Category = db.Categories.FirstOrDefault(c => c.Title == "Компьютер")},
                    new Subcategory{Title = "Игровой", Category = db.Categories.FirstOrDefault(c => c.Title == "Ноутбук")},
                    new Subcategory{Title = "Обычный", Category = db.Categories.FirstOrDefault(c => c.Title == "Ноутбук")}
                };
                db.Subcategories.AddRange(subcategories);
                db.SaveChanges();
            }
            if (!db.Products.Any())
            {
                var products = new List<Product>();
                for (int i = 0; i <= 25; i++)
                {
                    var product = new Product
                    {
                        Title = $"Компьютер игровой{i}",
                        Description = $"О компьютере игровом{i}",
                        Quantity = 1000,
                        Price = (decimal)1000 + i,
                        CreatedAt = DateTime.UtcNow,
                        Subcategory = db.Subcategories.FirstOrDefault(s => s.Title == "Игровой" && s.Category.Title == "Компьютер"),
                        ImageUrl = "https://pbs.twimg.com/media/D8tUXF5WkAMKa_S.jpg:large"
                    };
                    product.AttributeValues = db.Attributes.Select(a => new AttributeValue { Attribute = a, Product = product, Value = "x" }).ToList();

                    products.Add(product);
                }
                for (int i = 0; i <= 25; i++)
                {
                    var product = new Product
                    {
                        Title = $"Компьютер обычный{i}",
                        Description = $"О компьютере обычном{i}",
                        Quantity = 1000,
                        Price = (decimal)1000 + i,
                        CreatedAt = DateTime.UtcNow,
                        Subcategory = db.Subcategories.FirstOrDefault(s => s.Title == "Обычный" && s.Category.Title == "Компьютер"),
                        ImageUrl = "https://a5-express.ru/upload/iblock/543/64z7hsfboagogwf26zmgm0jvbwoi0whw.jpg"
                    };
                    product.AttributeValues = db.Attributes.Select(a => new AttributeValue { Attribute = a, Product = product, Value = "x" }).ToList();

                    products.Add(product);
                }
                for (int i = 0; i <= 25; i++)
                {
                    var product = new Product
                    {
                        Title = $"Ноутбук обычный{i}",
                        Description = $"О ноутбуке обычном{i}",
                        Quantity = 1000,
                        Price = (decimal)1000 + i,
                        CreatedAt = DateTime.UtcNow,
                        Subcategory = db.Subcategories.FirstOrDefault(s => s.Title == "Обычный" && s.Category.Title == "Ноутбук"),
                        ImageUrl = "https://idedal.ru/images/notebook.png"
                    };
                    product.AttributeValues = db.Attributes.Select(a => new AttributeValue { Attribute = a, Product = product, Value = "x" }).ToList();

                    products.Add(product);
                }
                for (int i = 0; i <= 25; i++)
                {
                    var product = new Product
                    {
                        Title = $"Ноутбук игровой{i}",
                        Description = $"О ноутбуке игровом{i}",
                        Quantity = 1000,
                        Price = (decimal)1000 + i,
                        CreatedAt = DateTime.UtcNow,
                        Subcategory = db.Subcategories.FirstOrDefault(s => s.Title == "Игровой" && s.Category.Title == "Ноутбук"),
                        ImageUrl = "https://купибук.рф/upload/iblock/ccd/ccdf22e65a53567a0e418668e59402de.jpg"
                    };
                    product.AttributeValues = db.Attributes.Select(a => new AttributeValue { Attribute = a, Product = product, Value = "x" }).ToList();

                    products.Add(product);
                }
                db.Products.AddRange(products);
                db.SaveChanges();
            }
            if (!db.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "Test",
                        SurName = "Test",
                        MiddleName = "Test",
                        PhoneNumber ="12345678901",
                        BirthDate =DateTime.UtcNow,
                        CreatedAt =DateTime.UtcNow,
                    },
                    new User 
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "TestTest",
                        SurName = "TestTest",
                        MiddleName = "TestTest",
                        PhoneNumber ="12345678901",
                        BirthDate =DateTime.UtcNow,
                        CreatedAt =DateTime.UtcNow,
                    }
                };
                var carts = new List<Cart>
                {
                    new Cart { User = users[0] },
                    new Cart { User = users[1] },
                };
                db.Carts.AddRange(carts);   
                db.Users.AddRange(users);
                db.SaveChanges();
            }
            if (!db.Feedbacks.Any())
            {
                List<Feedback> feedbacks = new List<Feedback>();
                foreach (var product in db.Products.ToList())
                {
                    feedbacks.AddRange(new List<Feedback>
                    { 
                        new Feedback{Text = "Good", Rating = 5, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = product},
                        new Feedback{Text = "Very Good", Rating = 4, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = product},
                        new Feedback{Text = "Amazing", Rating = 5, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = product},
                        new Feedback{Text = "Good", Rating = 5, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = product},
                        new Feedback{Text = "Bad", Rating = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = product},
                        new Feedback{Text = "Et harum quidem rerum facilis est et expedita distinctio", Rating = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = product},
                    });
                }
                db.Feedbacks.AddRange(feedbacks);
                db.SaveChanges();
            }
            if (!db.Comments.Any())
            {
                List<Comment> comments = new List<Comment>();
                foreach (var feedback in db.Feedbacks.ToList())
                {
                    comments.AddRange(new List<Comment>
                    {
                        new Comment{Text = "Agree", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = feedback.Product, Feedback = feedback},
                        new Comment{Text = "Disagree", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = feedback.Product, Feedback = feedback},
                        new Comment{Text = "Lol", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = feedback.Product, Feedback = feedback},
                        new Comment{Text = "Et harum quidem rerum facilis est et expedita distinctio", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = feedback.Product, Feedback = feedback}
                    });
                }
                db.Comments.AddRange(comments);
                db.SaveChanges();
                bool swap = true;
                List<Comment> ccomments = new List<Comment>();
                foreach (var comment in db.Comments.ToList())
                {
                    if (swap)
                    {
                        ccomments.AddRange(new List<Comment>
                    {
                        new Comment{Text = "Agree", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = comment.Product, Feedback = comment.Feedback, ParentComment = comment},
                        new Comment{Text = "Disagree", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = comment.Product, Feedback = comment.Feedback, ParentComment = comment},
                        new Comment{Text = "Lol", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = comment.Product, Feedback = comment.Feedback, ParentComment = comment},
                        new Comment{Text = "Et harum quidem rerum facilis est et expedita distinctio", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow, User = db.Users.FirstOrDefault(), Product = comment.Product, Feedback = comment.Feedback, ParentComment = comment}
                    });
                    }
                    swap = !swap;
                }
                db.Comments.AddRange(ccomments);
                db.SaveChanges();
            }
        }
    }
}
