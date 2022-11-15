using BookstoreManager.Domain.dto.register;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.BookService.Command.Register
{
    public class RegisterBookService : IRegisterBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<RegisterRequest> _validator;
        public RegisterBookService(IBookRepository bookRepository, IValidator<RegisterRequest> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
        }

        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            #region ValidatorRequest
            var validator = _validator.Validate(request);

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion

            var book = new Domain.Entities.Book
            {
                Name = request.Name,
                Description = request.Description,
                Author = request.Author,
                Active = true,
                CreateAt = DateTime.Now,
            };
            
           await _bookRepository.Add(book);

            return new RegisterResponse("cadastrado com sucesso !");
        }
    }
}
