using BookstoreManager.Application.LogErrorService.Command.ToView;
using BookstoreManager.Application.LogErrorService.Querie.GetAll;
using BookstoreManager.Domain.dto.ErrorDto;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookstoreManager.WebApi.Controller
{
    /// <summary>
    /// ErrorController
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class LogErrorController : ControllerBase
    {
        private readonly IGetAllLogErrorService _getAllLogErrorService;
        private readonly IViewLogErrorService _viewLogErrorService;
        public LogErrorController(IGetAllLogErrorService getAllLogErrorService,
                                  IViewLogErrorService viewLogErrorService)
        {
            _getAllLogErrorService = getAllLogErrorService;
            _viewLogErrorService = viewLogErrorService;
        }


        /// <summary>
        /// Get All 
        /// </summary>   
        /// <param name="page">has default value</param>
        /// <param name="pageSize">value per page</param>
        /// <param name="search">optional</param>    
        [HttpPost("api/[controller]/GetAll")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<GetAllLogErrorResponse>))]
        public async Task<IActionResult> GetAll(string? search, int page =1, int pageSize =10)
        {
            try
            {
                var result = await _getAllLogErrorService.GetAll(new GetAllErrorLogRequest
                {
                    Search = search,
                    Page = page,
                    PageSize = pageSize

                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        /// <summary>
        /// To view Logs 
        /// </summary>   
         /// <param name="Id">get by id</param>  
        [HttpPost("api/[controller]/ToView")]

        public async Task<IActionResult> ToView(int Id)
        {
            try
            {
                var result = await _viewLogErrorService.ToView(Id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
