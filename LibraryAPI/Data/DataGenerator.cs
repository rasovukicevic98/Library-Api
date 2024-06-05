using Bogus;
using LibraryAPI.Constants;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public class DataGenerator
    {

        public List<User> GenerateUsers(int count)
        {
            var userIds = 40;

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => userIds++.ToString())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.UserName, f => f.Name.FullName());
            userIds++;
            return userFaker.Generate(count);
        }

        public List<Author> GenerateAuthors(int count)
        {
            var authorIds = 1;

            var authorFaker = new Faker<Author>()                
                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                .RuleFor(a => a.LastName, f => f.Name.LastName()); ;

            return authorFaker.Generate(count);
        }

        public List<Book> GenerateBooks(int count, List<Author> authors)
        {
            var bookIds = 1;

            var bookFaker = new Faker<Book>()
                .RuleFor(b => b.Title, f => f.Lorem.Sentence(3))
                .RuleFor(b => b.ISBN, f => f.Commerce.Ean13())
                .RuleFor(b => b.Genre, f => f.PickRandom<Genres>())
                .RuleFor(b => b.NumberOfPages, f => f.Random.Int(100, 1000))
                .RuleFor(b => b.YearPublished, f => f.Date.Past(30).Year)
                .RuleFor(b => b.TotalCopies, f => f.Random.Int(1, 20))
                .RuleFor(b => b.CreatedAt, f => f.Date.Past(1))
                .RuleFor(b => b.ModifiedAt, f => f.Date.Past(1))
                .RuleFor(b => b.IsDeleted, f => false)
                .RuleFor(b => b.Authors, f => f.PickRandom(authors, f.Random.Int(1, 3)).ToList());

            return bookFaker.Generate(count);
        }

        public List<Review> GenerateReviews(int count, List<User> users, List<Book> books, DataContext context)
        {
            foreach (var book in books)
            {
                if (context.Entry(book).State == EntityState.Detached)
                {
                    context.Books.Attach(book);
                }
            }
            var reviewFaker = new Faker<Review>()
                .RuleFor(r => r.UserId, f => f.PickRandom(users).Id)
                .RuleFor(r => r.BookId, f => f.PickRandom(books).Id)
                .RuleFor(r => r.Rating, f => f.Random.Int(1, 10))
                .RuleFor(r => r.Comment, f => f.Lorem.Sentence());

            var uniqueReviews = new HashSet<(string UserId, int BookId)>();
            var reviews = new List<Review>();

            for (int i = 0; i < count; i++)
            {
                var review = reviewFaker.Generate();
                if (uniqueReviews.Add((review.UserId, review.BookId)))
                {
                    reviews.Add(review);
                }
            }

            return reviews;
        }
    }
}
