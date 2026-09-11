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
                new Movie { Id = 1, Title = "Inception", Type = "Movie", Director = "Christopher Nolan", Genre = "Sci-Fi", ReleaseYear = 2010, Img = "https://image.tmdb.org/t/p/w500/edv5CZvWj09upOsy2Y6IwDhK8bt.jpg", Description = "A thief who steals corporate secrets..." },
                new Movie { Id = 2, Title = "Interstellar", Type = "Movie", Director = "Christopher Nolan", Genre = "Sci-Fi", ReleaseYear = 2014, Img = "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg", Description = "A team of explorers travel through a wormhole..." }
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