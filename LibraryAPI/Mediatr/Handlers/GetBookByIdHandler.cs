using LibraryAPI.Contracts.Repositories;
using LibraryAPI.Mediatr.Requests;
using LibraryAPI.Models;
using MediatR;

namespace LibraryAPI.Mediatr.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepository;
        public GetBookByIdHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            return _bookRepository.GetById(request.Id);
            
        }
    }
}
