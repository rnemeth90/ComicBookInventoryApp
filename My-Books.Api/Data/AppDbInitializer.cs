using My_Books.Api.Models;

namespace My_Books.Api.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();

                if (!context.Books.Any())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        context.Books.AddRange(new Book()
                        {
                            Title = $"Book #{i}",
                            Description = $"A description for book #{i}",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            DateAdded = DateTime.Now,
                            Rating = new Random().Next(0,10),
                            Genre = "Science Fiction",
                            Author = "Robert Kirkman",
                            CoverUrl = "...."
                        });
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
