namespace DoroTechCSharpTest.Domain.Entities.Validations.BookValidator
{
    public class BookUpdateValidation : BookValidation
    {
        public BookUpdateValidation()
        {
            ValidateId();
            ValidateTitle();
            ValidateAuthor();
            ValidateCode();
            ValidateReleaseYear();
        }
    }
}