using BookstoreManager.Application.Authentication.Login;
using BookstoreManager.Application.AuthenticationService.Register;
using BookstoreManager.Application.BookService.Command.Delete;
using BookstoreManager.Application.BookService.Command.Register;
using BookstoreManager.Application.BookService.Command.Update;
using BookstoreManager.Application.BookService.Querie.GetAll;
using BookstoreManager.Application.LogErrorService.Command.Delete;
using BookstoreManager.Application.LogErrorService.Command.ToView;
using BookstoreManager.Application.LogErrorService.Querie.GetAll;
using BookstoreManager.Application.LogErrorService.Register;
using BookstoreManager.Application.Validator.AuthenticatorValidator;
using BookstoreManager.Application.Validator.bookValidator;
using BookstoreManager.Application.Validator.LogErrorValidator;
using BookstoreManager.Domain.dto.authenticationDto;
using BookstoreManager.Domain.dto.register;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BookstoreManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(
           this IServiceCollection services)
        {
            services.AddScoped<IRegisterBookService, RegisterBookService>();
            services.AddScoped<IValidator<RegisterRequest>, RegisterCheckRequestValidator>();
            services.AddScoped<IUpdateBookService, UpdateBookService>();
            services.AddScoped<IRemoveBookService, RemoveBookService>();
            services.AddScoped<IGetAllBookService, GetAllBookService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IValidator<RegisterUserRequest>, RegisterCheckRequestValidators>();
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<IViewLogErrorService, ViewLogErrorService>();
            services.AddScoped<IValidator<CheckhasId>, UpdateCheckHasIdValidator>();
            services.AddScoped<IValidator<CheckhasIdDeleteRequest>, DeleteCheckHasIdValidator>();
            services.AddScoped<IGetAllLogErrorService, GetAllLogErrorService>();
            services.AddScoped<IRegisterLogErrorService, RegisterLogErrorService>();
            services.AddScoped<IDeleteErrorService, DeleteErrorService>();
           

            return services;
        }
    }
}