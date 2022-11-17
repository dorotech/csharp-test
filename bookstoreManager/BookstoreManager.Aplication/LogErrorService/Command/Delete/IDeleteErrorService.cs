using BookstoreManager.Domain.dto.ErrorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.Application.LogErrorService.Command.Delete
{
    public interface IDeleteErrorService
    {
        Task<DeleteErrorResponse> Delete(int id);
    }
}
