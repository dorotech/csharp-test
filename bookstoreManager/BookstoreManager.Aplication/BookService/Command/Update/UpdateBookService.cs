using BookstoreManager.Application.Validator.bookValidator;
using BookstoreManager.Domain.dto.update;
using BookstoreManager.Domain.Entities;
using BookstoreManager.Repository.Interface;
using FluentValidation;

namespace BookstoreManager.Application.BookService.Command.Update
{
    public class UpdateBookService : IUpdateBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IValidator<CheckhasId> _validator;
        public UpdateBookService(IBookRepository bookRepository,
                                 IValidator<CheckhasId> validator)
        {
            _bookRepository = bookRepository;
            _validator = validator;
        }
        public async Task<UpdateResponse> Update(UpdateRequest request)
        {
            #region ValidatorRequest
            var validator = _validator.Validate(new CheckhasId { Id = request.Id });

            if (!validator.IsValid)
                throw new Exception(string.Join(",", validator.Errors.Select(x => x.ErrorMessage)));
            #endregion


            var checkHasBook = await _bookRepository.GetByIdAsync(request.Id);


            checkHasBook.Name = request.Name ?? checkHasBook.Name;
            checkHasBook.Description = request.Description ?? checkHasBook.Description;
            checkHasBook.Author = request.Author ?? checkHasBook.Author;
            checkHasBook.CreateAt = checkHasBook.CreateAt;
            checkHasBook.UpdateAt = DateTime.Now;
            checkHasBook.Active = request.Active ;

            

             _bookRepository.Update(checkHasBook);

            return new  UpdateResponse("atualizado com sucesso");
        }
    }
}
