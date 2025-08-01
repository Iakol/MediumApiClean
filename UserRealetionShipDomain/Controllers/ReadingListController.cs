using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserRealetionShipDomain.Application.DTO;
using UserRealetionShipDomain.Application.Helper;
using UserRealetionShipDomain.Application.UseCases.SaveReadingListCases;

using UserRealetionShipDomain.Domain;
using UserRealetionShipDomain.Presentation.UseCases.ReadingListCases;

namespace UserRealetionShipDomain.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReadingListController(
        IGetUserSaveReadingListCase _getUserSaveReadingListCase,
        ISaveReadingListCase _saveReadingListCase,
        IUnSaveReadingListCase _unsaveReadingListCase
        ) : ControllerBase
    {
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserSaveReadingList(string? userId) 
        {
            if (string.IsNullOrEmpty(userId)) 
            {

                userId = User.FindFirst("sub")?.Value;
            }            

            Result<List<SaveReadingList>> result = await _getUserSaveReadingListCase.Handle(userId);

            if (result.IsSuccess) 
            {
                return Ok(result.Data);
            }

            return BadRequest(new ProblemDetails 
            {
                Title = "Save ReadingList fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });

            
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SaveReadingList([FromBody] string ReadingList)
        {
            string userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "UnSave ReadingList by User Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }

            Result result = await _saveReadingListCase.Handle(userId, ReadingList);
            if (result.IsSuccess)
            {               
                return Created();
            }       
    
            return BadRequest(new ProblemDetails 
            {
                Title = "Save ReadingList by User Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });    
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UnSaveReadingList([FromBody] string ReadingList)
        {
            string userId = User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) 
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "UnSave ReadingList by User Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,
                });
            }

            Result result = await _unsaveReadingListCase.Handle(userId, ReadingList);

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(new ProblemDetails
            {
                Title = "UnSave ReadingList by User Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }
      

    }
}
