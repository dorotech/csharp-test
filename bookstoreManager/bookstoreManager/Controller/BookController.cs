using BookstoreManager.Application.BookService.Command.Delete;
using BookstoreManager.Application.BookService.Command.Register;
using BookstoreManager.Application.BookService.Command.Update;
using BookstoreManager.Application.BookService.Querie.GetAll;
using BookstoreManager.Domain.dto.GetAll;
using BookstoreManager.Domain.dto.register;
using BookstoreManager.Domain.dto.update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookstoreManager.WebApi.Controller
{
    /// <summary>
    /// UserController
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly IRegisterBookService _registerBookService;
        private readonly IUpdateBookService _updateBookService;
        private readonly IRemoveBookService _removeBookService;
        private readonly IGetAllBookService _getAllBookService;
        public BookController(IRegisterBookService registerBookService,
                              IUpdateBookService updateBookService,
                              IRemoveBookService removeBookService,
                              IGetAllBookService getAllBookService)
        {
            _registerBookService = registerBookService;
            _updateBookService = updateBookService;
            _removeBookService = removeBookService;
            _getAllBookService = getAllBookService;
        }

        /// <summary>
        /// register books 
        /// </summary>
        [HttpPost("api/[controller]/Register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest request)
        {
            try
            {
                var result = await _registerBookService.Register(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        /// <summary>
        /// update books 
        /// </summary>
        [HttpPut("api/[controller]/Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody]UpdateRequest request)
        {
            try
            {
                var result = await _updateBookService.Update(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        /// <summary>
        /// Delete  book
        /// </summary>
        [HttpDelete("api/[controller]/Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _removeBookService.Remove(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        /// <summary>
        /// Get All books 
        /// </summary>
        /// <param name="request.Page">has default value</param>
        /// <param name="request.PageSize">has default value</param>
        /// <param name="request.Search">optional</param>      
         
        [SwaggerResponse(statusCode: 200,  type: typeof(List<GetAllBookResponse>))]
        [HttpGet("api/[controller]/GetAll")]
        public async Task<IActionResult> GetAll([FromHeader]GetAllBookRequest request)
        {
            try
            {
                var result = await _getAllBookService.GetAll(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }


    }
}
