using APIBook.Model;
using Microsoft.EntityFrameworkCore;


namespace APIBook.Services
{
    public static class DataBaseMigrateService
    {
        public static void MigrationInitialsation(this IApplicationBuilder applicationBuilder)
        {
            using (var service_scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var service_db = service_scope.ServiceProvider.GetService<BookContext>();
          
                if(service_db != null)
                    service_db.Database.Migrate(); //migrar o banco de dados caso não exista
            }
        }
    }
}
