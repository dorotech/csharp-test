using api.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.Repositories.category
{
    public interface ICategoryRepository
    { 
        Task<Category> Post(Category category);
    }
}
