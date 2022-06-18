using ComicBookInventory.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ComicBookInventory.DataAccess
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder appBuilder)
        {
            using (var serviceScope = appBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApiDbContext>();

                if (!context.ComicBooks.Any())
                {
                    for (int i = 0; i < 100; i++)
                    {
                        context.ComicBooks.AddRange(new ComicBook()
                        {
                            Title = $"Ant-Man Volume #{i}",
                            Description = $"A comic starring everyone's favorite size-changing hero.",
                            IsRead = true,
                            DateRead = DateTime.Now.AddDays(-10),
                            DateAdded = DateTime.Now,
                            Rating = new Random().Next(0, 10),
                            Genre = "Science Fiction",
                            CoverUrl = "...."
                        });
                    }
                    context.SaveChanges();
                }

                if (!context.Authors.Any())
                {
                    context.Authors.Add(new Author()
                    {
                        FullName = "Stan Lee"
                    });

                    context.Authors.Add(new Author()
                    {
                        FullName = "Jack Kirby"
                    });
                    context.SaveChanges();
                }

                if (!context.Characters.Any())
                {
                    context.Characters.Add(new Character()
                    {
                        FullName = "Ant-Man",
                        Alias = "Giant Man",
                        Species = "Human",
                        PrimaryAbility = "Growing",
                        SecondaryAbility = "Shrinking",
                        IsAlive = true,
                        Weapon = "Pim Particles, suit",
                    });

                    context.Characters.Add(new Character()
                    {
                        FullName = "Iron Man",
                        Alias = "War Machine",
                        Species = "Human",
                        PrimaryAbility = "Intelligence",
                        SecondaryAbility = "Wealth",
                        IsAlive = true,
                        Weapon = "Suit",
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}
