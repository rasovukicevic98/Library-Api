using LibraryAPI.Models;
using MediatR;

namespace LibraryAPI.Mediatr.Requests
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }

        public GetBookByIdQuery(int id)
        {

            Id = id;
        }
    }
}
