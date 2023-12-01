using API.Extensions;
using Application.Common.Responses;
using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using CrossCutting.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin.V1;

[Tags("Categories - Admin")]
public class CategoriesController(ILogger<CategoriesController> logger) : ApiAdminControllerBaseV1
{
    /// <summary>
    /// Create a category
    /// </summary>
    /// <param name="command">Command to create category.</param>
    /// <returns>Returns the new category</returns>
    /// <response code="201">Success create new Category.</response>
    /// <response code="400">Cannot or will not process the request to create category.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPost]
    public async Task<ActionResult<ReturnMessage<CategoryResponse>>> CreateAsync([FromBody] CreateCategoryCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }

    /// <summary>
    /// Update a category
    /// </summary>
    /// <param name="command">Command to create category.</param>
    /// <returns>Returns the new category</returns>
    /// <response code="200">Success updated category.</response>
    /// <response code="400">Cannot or will not process the request to create category.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="409">Conflicts in the request.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ReturnMessage<CategoryResponse>), StatusCodes.Status500InternalServerError)]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnMessage<CategoryResponse>>> UpdateAsync(UpdateCategoryCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
    
    /// <summary>
    /// Delete a category
    /// </summary>
    /// <param name="command">Command to delete category.</param>
    /// <response code="204">Success delete category.</response>
    /// <response code="401">Not authenticated.</response>
    /// <response code="403">Not Access not authorized.</response>
    /// <response code="404">Category not exists.</response>
    /// <response code="500">Any exception occurred.</response>
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Nullable), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ReturnMessage), StatusCodes.Status500InternalServerError)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnMessage>> DeleteAsync(DeleteCategoryCommand command)
    {
        var result = await Mediator.Send(command);

        return result.ToActionResult(logger);
    }
}