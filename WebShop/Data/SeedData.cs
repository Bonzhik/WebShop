using System.Net;
using System.Reflection;
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
            if(!db.Subcategories.Any())
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
                for (int i = 0; i <= 25 ; i++)
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
                    product.AttributeValues = db.Attributes.Select(a => new AttributeValue {Attribute = a, Product = product, Value = "x"}).ToList();

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
        }
    }
}
