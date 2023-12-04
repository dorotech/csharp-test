namespace DoroTechCSharpTest.Domain.Entities.Validations.BookValidator
{
    public class BookRegisterValidation : BookValidation
    {
        public BookRegisterValidation()
        {
            ValidateTitle();
            ValidateAuthor();
            ValidateCode();
            ValidateReleaseYear();
        }
    }
}