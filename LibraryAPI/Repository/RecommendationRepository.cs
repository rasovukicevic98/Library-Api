using LibraryAPI.Data;

namespace LibraryAPI.Repository
{
    public class RecommendationRepository
    {
        private readonly DataContext _context;

        public RecommendationRepository(DataContext context)
        {
            _context = context;
        }

    }
}
