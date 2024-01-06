dotnet ef migrations add InitialCreate --project src\DoroTech.BookStore.Infrastructure --startup-project src\DoroTech.BookStore.Api

dotnet ef migrations remove --project src\DoroTech.BookStore.Infrastructure --startup-project src\DoroTech.BookStore.Api 