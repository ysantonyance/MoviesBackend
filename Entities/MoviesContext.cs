using Microsoft.EntityFrameworkCore;
using MoviesBackend.Entities;
using System.Reflection.Emit;

namespace MoviesBackend.Entities
{
    public class MoviesContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options) 
        {
            if (Database.EnsureCreated())
            {
                Movies?.Add(new Movie
                {
                    Title = "Backrooms",
                    Type = "Movie",
                    Director = "David F. Sandberg",
                    Genre = "Psychological Horror, Psychological Thriller, Supernatural Horror, Horror, Sci-Fi, Thriller",
                    ReleaseYear = 2026,
                    Img = "/images/Backrooms.jpg",
                    Description = "After a therapist's patient disappears into a dimension beyond reality, she must venture into the unknown to save him."
                });

                Movies?.Add(new Movie
                {
                    Title = "Girl from Nowhere",
                    Type = "TV Series",
                    Director = "Jatuphong Rungrueangdechaphat, S Khomkrit Treewimol, Pairach Khumwan, Sitisiri Mongkolsiri, Varayu Ruksku, Siwawut Sewatanon, Chaianan Soijumpa, Apiwat Supateerapong, T-Thawat Taifayongvichit, Pokpong Pairach Khumwan, Paween Purijitpanya, Surawut Tungkharak",
                    Genre = "Thai, Psychological Drama, Teen Horror, Crime, Drama, Fantasy, Horror, Mystery, Thriller",
                    ReleaseYear = 2018,
                    Img = "/images/GirlFromNowhere.jpg",
                    Description = "A mysterious, clever girl named Nanno transfers to different schools, exposing the lies and misdeeds of the students and faculty at every turn."
                });

                Movies?.Add(new Movie
                {
                    Title = "A Minecraft Movie",
                    Type = "Movie",
                    Director = "Jared Hess",
                    Genre = "High-Concept Comedy, Quest, Slapstick, Survival, Action, Adventure, Comedy, Family, Fantasy",
                    ReleaseYear = 2025,
                    Img = "/images/AMinecraftMovie.jpg",
                    Description = "Four misfits are suddenly pulled through a mysterious portal into a bizarre cubic wonderland that thrives on imagination. To get back home they'll have to master this world while embarking on a quest with an unexpected expert crafter."
                });

                Movies?.Add(new Movie
                {
                    Title = "Arcane",
                    Type = "TV Series",
                    Director = "Arnaud Delord, Pascal Charrue, Bart Maunoury, Marietta Ren, Christelle Abgrall, Etienne Mattera",
                    Genre = "Action Epic, Adult Animation, Computer Animation, Dark Fantasy, Dystopian Sci-Fi, Epic, Fantasy, Political Drama, Psychological Drama, Steampunk",
                    ReleaseYear = 2021,
                    Img = "/images/Arcane.jpg",
                    Description = "Amid the stark discord of twin cities Piltover and Zaun, two sisters fight on rival sides of a war between magic technologies and clashing convictions."
                });

                Movies?.Add(new Movie
                {
                    Title = "Michael",
                    Type = "Movie",
                    Director = "Antoine Fuqua",
                    Genre = "Coming-of-Age, Docudrama, Period Drama, Psychological Drama, Drama, Biography, History, Music",
                    ReleaseYear = 2026,
                    Img = "/images/Michael.jpg",
                    Description = "The early life of musician Michael Jackson, from the discovery of his talent as the lead of the Jackson Five to the artist whose creative ambition fueled a pursuit to become the biggest entertainer in the world."
                });

                Movies?.Add(new Movie
                {
                    Title = "Obsession",
                    Type = "Movie",
                    Director = "Curry Barker",
                    Genre = "Dark Romance, Psychological Horror, Psychological Thriller, Supernatural Horror, Horror, Romance, Thriller",
                    ReleaseYear = 2025,
                    Img = "/images/Obsession.jpg",
                    Description = "After breaking the mysterious \"One Wish Willow\" to win his crush's heart, a hopeless romantic finds himself getting exactly what he asked for but soon discovers that some desires come at a dark, sinister price."
                });

                Movies?.Add(new Movie
                {
                    Title = "Avatar: The Last Airbender",
                    Type = "TV Series",
                    Director = "Craig Mazin, Neil Druckmann, Kantemir Balagov, Ali Abbasi, Peter Hoar, Jasmila Žbanić, Lisa Cholodenko, Jeremy Webb",
                    Genre = "Avatar: The Last Airbender, Adventure Epic, Epic, Fantasy Epic, Globetrotting Adventure, Hand-Drawn Animation, Martial Arts, Quest, Superhero, Supernatural Fantasy",
                    ReleaseYear = 2005,
                    Img = "/images/AvatarTheLastAirbender.jpg",
                    Description = "After a century frozen in ice, young Avatar Aang journeys with friends to master the four elements and bring balance to a world at war with the Fire Nation."
                });

                Movies?.Add(new Movie
                {
                    Title = "Dexter",
                    Type = "Tv Series",
                    Director = "James Manos Jr.",
                    Genre = "Dark Comedy, Psychological Drama, Psychological Thriller, Serial Killer, Suspense Mystery Whodunnit, Crime, Dtama, Mystery, Thriller",
                    ReleaseYear = 2006,
                    Img = "/images/Dexter.jpg",
                    Description = "He's smart. He's lovable. He's Dexter Morgan, America's favorite serial killer, who spends his days solving crimes and his nights committing them.\r\n\r\n"
                });

                Movies?.Add(new Movie
                {
                    Title = "Black Swan",
                    Type = "Movie",
                    Director = "Darren Aronofsky",
                    Genre = "Epic, Psychological Drama, Psychological Thriller, Showbiz Drama, Drama, Thriller",
                    ReleaseYear = 2010,
                    Img = "/images/BlackSwan.jpg",
                    Description = "An insecure ballerina finally gets the role she has always dreamed of, but as the pressures mount her line between reality and illusion starts to blur.\r\n\r\n"
                });

                Movies?.Add(new Movie
                {
                    Title = "Spirited Away",
                    Type = "Movie",
                    Director = "Hayao Miyazaki",
                    Genre = "Japanese, Anime, Coming-of-Age, Fairy Tale, Hand-Drawn Animation, Isekai, Supernatural Fantasy, Adventure, Animation, Family",
                    ReleaseYear = 2001,
                    Img = "/images/SpiritedAway.jpg",
                    Description = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches and spirits, and where humans are changed into beasts.\r\n\r\n"
                });

                SaveChanges();
            }
        }
    }
}
