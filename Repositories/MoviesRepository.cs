using Firebase.Database;
using Firebase.Database.Query;
using MoviesBackend.Entities;
using MoviesBackend.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MoviesBackend.Repository
{
    public class MoviesRepository : IRepository<Movie>
    {
        private readonly FirebaseClient _firebaseClient;
        private const string CollectionName = "movies";
        private const string MetaNode = "movies_meta";

        private readonly Lazy<Task> _seedTask;

        public MoviesRepository(IConfiguration config)
        {
            string databaseUrl = config["FIREBASE_DATABASE_URL"]
                ?? config["Firebase__DatabaseUrl"]
                ?? config["Firebase:DatabaseUrl"]
                ?? throw new InvalidOperationException("Firebase Database URL не налаштовано.");

            _firebaseClient = new FirebaseClient(databaseUrl);

            _seedTask = new Lazy<Task>(EnsureSeededAsync);
        }

        private async Task EnsureSeededAsync()
        {
            bool? alreadySeeded = await _firebaseClient
                .Child(MetaNode)
                .Child("seeded")
                .OnceSingleAsync<bool?>();

            if (alreadySeeded == true)
                return;

            var existing = await _firebaseClient
                .Child(CollectionName)
                .OnceSingleAsync<Dictionary<string, Movie>>();

            if (existing == null || existing.Count == 0)
            {
                foreach (var movie in GetSeedData())
                {
                    await _firebaseClient
                        .Child(CollectionName)
                        .Child(movie.Id.ToString())
                        .PutAsync(movie);
                }
            }

            await _firebaseClient
                .Child(MetaNode)
                .Child("seeded")
                .PutAsync(true);
        }

        private Task EnsureSeedWithRetryAsync() => _seedTask.Value;

        private static List<Movie> GetSeedData()
        {
            return new List<Movie>
            {
                new Movie {
                    Id = 1,
                    Title = "Backrooms",
                    Type = "Movie",
                    Director = "David F. Sandberg",
                    Genre = "Psychological Horror, Psychological Thriller, Supernatural Horror, Horror, Sci-Fi, Thriller",
                    ReleaseYear = 2026,
                    Img = "/images/Backrooms.jpg",
                    Description = "After a therapist's patient disappears into a dimension beyond reality, she must venture into the unknown to save him."
                },
                new Movie { 
                    Id = 2,
                    Title = "Girl from Nowhere",
                    Type = "TV Series",
                    Director = "Jatuphong Rungrueangdechaphat, S Khomkrit Treewimol, Pairach Khumwan, Sitisiri Mongkolsiri, Varayu Ruksku, Siwawut Sewatanon, Chaianan Soijumpa, Apiwat Supateerapong, T-Thawat Taifayongvichit, Pokpong Pairach Khumwan, Paween Purijitpanya, Surawut Tungkharak",
                    Genre = "Thai, Psychological Drama, Teen Horror, Crime, Drama, Fantasy, Horror, Mystery, Thriller",
                    ReleaseYear = 2018,
                    Img = "/images/GirlFromNowhere.jpg",
                    Description = "A mysterious, clever girl named Nanno transfers to different schools, exposing the lies and misdeeds of the students and faculty at every turn."
                },
                new Movie {
                    Id = 3,
                    Title = "A Minecraft Movie",
                    Type = "Movie",
                    Director = "Jared Hess",
                    Genre = "High-Concept Comedy, Quest, Slapstick, Survival, Action, Adventure, Comedy, Family, Fantasy",
                    ReleaseYear = 2025,
                    Img = "/images/AMinecraftMovie.jpg",
                    Description = "Four misfits are suddenly pulled through a mysterious portal into a bizarre cubic wonderland that thrives on imagination. To get back home they'll have to master this world while embarking on a quest with an unexpected expert crafter."
                },
                new Movie {
                    Id = 4,
                    Title = "Arcane",
                    Type = "TV Series",
                    Director = "Arnaud Delord, Pascal Charrue, Bart Maunoury, Marietta Ren, Christelle Abgrall, Etienne Mattera",
                    Genre = "Action Epic, Adult Animation, Computer Animation, Dark Fantasy, Dystopian Sci-Fi, Epic, Fantasy, Political Drama, Psychological Drama, Steampunk",
                    ReleaseYear = 2021,
                    Img = "/images/Arcane.jpg",
                    Description = "Amid the stark discord of twin cities Piltover and Zaun, two sisters fight on rival sides of a war between magic technologies and clashing convictions."
                },
                new Movie {
                    Id = 5,
                    Title = "Michael",
                    Type = "Movie",
                    Director = "Antoine Fuqua",
                    Genre = "Coming-of-Age, Docudrama, Period Drama, Psychological Drama, Drama, Biography, History, Music",
                    ReleaseYear = 2026,
                    Img = "/images/Michael.jpg",
                    Description = "The early life of musician Michael Jackson, from the discovery of his talent as the lead of the Jackson Five to the artist whose creative ambition fueled a pursuit to become the biggest entertainer in the world."
                },
                new Movie {
                    Id = 6,
                    Title = "Obsession",
                    Type = "Movie",
                    Director = "Curry Barker",
                    Genre = "Dark Romance, Psychological Horror, Psychological Thriller, Supernatural Horror, Horror, Romance, Thriller",
                    ReleaseYear = 2025,
                    Img = "/images/Obsession.jpg",
                    Description = "After breaking the mysterious \"One Wish Willow\" to win his crush's heart, a hopeless romantic finds himself getting exactly what he asked for but soon discovers that some desires come at a dark, sinister price."
                },
                new Movie {
                    Id = 7,
                    Title = "Avatar: The Last Airbender",
                    Type = "TV Series",
                    Director = "Craig Mazin, Neil Druckmann, Kantemir Balagov, Ali Abbasi, Peter Hoar, Jasmila Žbanić, Lisa Cholodenko, Jeremy Webb",
                    Genre = "Avatar: The Last Airbender, Adventure Epic, Epic, Fantasy Epic, Globetrotting Adventure, Hand-Drawn Animation, Martial Arts, Quest, Superhero, Supernatural Fantasy",
                    ReleaseYear = 2005,
                    Img = "/images/AvatarTheLastAirbender.jpg",
                    Description = "After a century frozen in ice, young Avatar Aang journeys with friends to master the four elements and bring balance to a world at war with the Fire Nation."
                },
                new Movie {
                    Id = 8,
                    Title = "Dexter",
                    Type = "Tv Series",
                    Director = "James Manos Jr.",
                    Genre = "Dark Comedy, Psychological Drama, Psychological Thriller, Serial Killer, Suspense Mystery Whodunnit, Crime, Dtama, Mystery, Thriller",
                    ReleaseYear = 2006,
                    Img = "/images/Dexter.jpg",
                    Description = "He's smart. He's lovable. He's Dexter Morgan, America's favorite serial killer, who spends his days solving crimes and his nights committing them.\r\n\r\n"
                },
                new Movie {
                    Id = 9,
                    Title = "Black Swan",
                    Type = "Movie",
                    Director = "Darren Aronofsky",
                    Genre = "Epic, Psychological Drama, Psychological Thriller, Showbiz Drama, Drama, Thriller",
                    ReleaseYear = 2010,
                    Img = "/images/BlackSwan.jpg",
                    Description = "An insecure ballerina finally gets the role she has always dreamed of, but as the pressures mount her line between reality and illusion starts to blur.\r\n\r\n"
                },
                new Movie {
                    Id = 10,
                    Title = "Spirited Away",
                    Type = "Movie",
                    Director = "Hayao Miyazaki",
                    Genre = "Japanese, Anime, Coming-of-Age, Fairy Tale, Hand-Drawn Animation, Isekai, Supernatural Fantasy, Adventure, Animation, Family",
                    ReleaseYear = 2001,
                    Img = "/images/SpiritedAway.jpg",
                    Description = "During her family's move to the suburbs, a sullen 10-year-old girl wanders into a world ruled by gods, witches and spirits, and where humans are changed into beasts.\r\n\r\n"
                },
            };
        }

        public async Task<List<Movie>> GetAllAsync()
        {
            await EnsureSeedWithRetryAsync();

            try
            {
                var moviesDict = await _firebaseClient
                    .Child(CollectionName)
                    .OnceSingleAsync<Dictionary<string, Movie>>();

                if (moviesDict == null) return new List<Movie>();

                return moviesDict
                    .Where(pair => pair.Value != null)
                    .Select(pair =>
                    {
                        var movie = pair.Value;
                        if (movie.Id == 0 && int.TryParse(pair.Key, out int id))
                        {
                            movie.Id = id;
                        }
                        return movie;
                    }).ToList();
            }
            catch
            {
                var moviesList = await _firebaseClient
                    .Child(CollectionName)
                    .OnceSingleAsync<List<Movie>>();

                if (moviesList == null) return new List<Movie>();

                return moviesList.Where(m => m != null).ToList();
            }
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            await EnsureSeedWithRetryAsync();

            var movie = await _firebaseClient
                .Child(CollectionName)
                .Child(id.ToString())
                .OnceSingleAsync<Movie>();

            if (movie != null)
            {
                movie.Id = id;
            }
            return movie;
        }

        public async Task CreateAsync(Movie entity)
        {
            await EnsureSeedWithRetryAsync();

            if (entity.Id == 0)
            {
                var list = await GetAllAsync();
                entity.Id = list.Any() ? list.Max(m => m.Id) + 1 : 1;
            }

            await _firebaseClient
                .Child(CollectionName)
                .Child(entity.Id.ToString())
                .PutAsync(entity);
        }

        public async Task UpdateAsync(Movie entity)
        {
            await EnsureSeedWithRetryAsync();

            await _firebaseClient
                .Child(CollectionName)
                .Child(entity.Id.ToString())
                .PutAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await EnsureSeedWithRetryAsync();

            await _firebaseClient
                .Child(CollectionName)
                .Child(id.ToString())
                .DeleteAsync();
        }

        public bool IsExisting(int id)
        {
            var movie = GetByIdAsync(id).GetAwaiter().GetResult();
            return movie != null;
        }
    }
}