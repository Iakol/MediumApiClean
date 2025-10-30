using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResponceDomain.Application.DTO;
using ResponceDomain.Domain;
using ResponceDomain.Presentation.Comand;
using ResponceDomain.Presentation.UseCases;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ResponceDomain.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController
        (
            IAddClapsToResponceCase _addClapsToResponceCase,
            IAddResponceCase _addResponceCase,
            IDeleteResponceCase _deleteResponceCase,
            IGetResponcesForItemCase _getResponcesForItem,
            IUpdateResponceCase _updateResponce
        ) : ControllerBase

    {

        [HttpPost("[action]")]
        public async Task<IActionResult> AddClapsToResponce([FromBody] AddClapsToResponceCommandData data)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                Result<int> result = await _addClapsToResponceCase.Handle(data, userId);

                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                return BadRequest(new ProblemDetails
                {
                    Title = "Add claps to responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Add claps to responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is not authorize",
                    Status = 400,
                });

            }
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> AddResponce([FromBody] CreateResponceCommandData NewResponce)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                Result result = await _addResponceCase.Handle(NewResponce, userId);

                if (result.IsSuccess)
                {
                    return Created();
                }
                return BadRequest(new ProblemDetails
                {
                    Title = "Add responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Add responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is not authorize",
                    Status = 400,
                });

            }
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> DeleteResponce([FromBody] int responceId)
        {
            string userId = User.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                Result result = await _deleteResponceCase.Handle(responceId, userId);

                if (result.IsSuccess)
                {
                    return Ok();
                }
                return BadRequest(new ProblemDetails
                {
                    Title = "Delete responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Delete responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is not authorize",
                    Status = 400,
                });

            }
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> GetItemsReponces([FromBody] string itemId)
        {
            Result < List < ResponceDTO >> result = await _getResponcesForItem.Handle(itemId);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Delete responce Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }
        [HttpPost("[action]")]

        public async Task<IActionResult> UpdateResponce([FromBody] UpdateResponceCommandData data) 
        {
            string userId = User.FindFirst("sub")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                Result result = await _updateResponce.Handle(data, userId);

                if (result.IsSuccess)
                {
                    return Ok();
                }
                return BadRequest(new ProblemDetails
                {
                    Title = "Update responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,
                });

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Update responce Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is not authorize",
                    Status = 400,
                });

            }
        }


    }
}
