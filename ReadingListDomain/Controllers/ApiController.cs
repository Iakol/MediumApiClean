using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingListDomain.Application.DTO;
using ReadingListDomain.Application.UseCases;
using ReadingListDomain.Domain;
using ReadingListDomain.Presentation.UserCases;

namespace ReadingListDomain.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApiController(
        ICreateReadingListCase _createReadingListCase,
        IDeleteReadingListCase _deleteReadingListCase,
        IGetListReadingListByCreatorIdCase _getListReadingListByCreatorIdCase,
        IGetListReadingListByIdsCase _getListReadingListByIdsCase,
        IGetReadingListCase _getReadingListCase,
        ISaveStoryToReadingListCase _saveStoryToReadingListCase,
        IUnSaveStoryFromReadingListCase _unSaveStoryFromReadingListCase,
        IUpdateNoteToSaveStoryInReadingListCase _updateNoteToSaveStoryInReadingListCase,
        IUpdateReadingListCase _updateReadingListCase,
        IUpdateReadingListPrivateCase _updateReadingListPrivateCase,
        IUpdateReadingListVisibleOfResponceCase _updateReadingListVisibleOfResponceCase

        ) : ControllerBase
    {

        //public Task<IActionResult> CreateConstantReadingListToUser()

        [HttpPost("[action]")]
        public async Task<IActionResult> createReadingList([FromBody] CreatePropsReadingListDTO readingList)
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _createReadingListCase.Handle(readingList, userId);

                if (result.IsSuccess)
                {
                    return Created();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Create reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Create reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }

        }


        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteReadingList([FromBody] string readlingListId)
        {
            var result = await _deleteReadingListCase.Handle(readlingListId);

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Delete reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,

                });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetListReadingListByCreatorId()
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _getListReadingListByCreatorIdCase.Handle(userId);

                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Get Reading Lists By CreatorId Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get Reading Lists By CreatorId Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetListReadingListByIds([FromBody] List<string> Ids)
        {
            var result = await _getListReadingListByIdsCase.Handle(Ids);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get ReadingLists By Ids Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,

                });
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetReadingList(string readingListId)
        {
            var result = await _getReadingListCase.Handle(readingListId);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Get by id reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = result.Error,
                    Status = 400,

                });
            }
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> SaveStoryToReadingList([FromBody]SaveStoryPropsDTO saveStoryProps) 
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _saveStoryToReadingListCase.Handle(saveStoryProps.storyId,userId, saveStoryProps.ReadinglistId);

                if (result.IsSuccess)
                {
                    return Created();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Save Story To Reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Save Story To Reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }

        }

        [HttpPost("[action]")] 
        public async Task<IActionResult> UnSaveStoryFromReadingList([FromBody] SaveStoryPropsDTO saveStoryProps) 
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _unSaveStoryFromReadingListCase.Handle(saveStoryProps.storyId, userId, saveStoryProps.ReadinglistId);

                if (result.IsSuccess)
                {
                    return Created();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "UnSave Story To Reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "UnSave Story To Reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateNoteToSaveStoryInReadingList(SaveStoryPropsDTO saveStoryProps) 
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _updateNoteToSaveStoryInReadingListCase.Handle(saveStoryProps.storyId, userId, saveStoryProps.ReadinglistId, saveStoryProps.Note);

                if (result.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Update Note Story To Reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Update Note Story To Reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateReadingListCase(CreatePropsReadingListDTO UpdateCredReadingList) 
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _updateReadingListCase.Handle(UpdateCredReadingList, userId);

                if (result.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Update Reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Update Reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateReadingListPrivate(UpdateReadingListPropsDTO readingListProps) 
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _updateReadingListPrivateCase.Handle(readingListProps.isPrivate!.Value, userId, readingListProps.ReadingListId);

                if (result.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Update isPrivate Reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Update isPrivate Reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }
        }

        [HttpPost("[action]")] 
        public async Task<IActionResult> UpdateReadingListVisibleOfResponce(UpdateReadingListPropsDTO readingListProps) 
        {
            string userId = User.FindFirst("sub")?.Value;

            if (!string.IsNullOrEmpty(userId))
            {
                var result = await _updateReadingListVisibleOfResponceCase.Handle(readingListProps.IsVisible!.Value, userId, readingListProps.ReadingListId);

                if (result.IsSuccess)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(new ProblemDetails
                    {
                        Title = "Update isVisible of Responces Reading List Fail",
                        Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                        Detail = result.Error,
                        Status = 400,

                    });
                }

            }
            else
            {
                return BadRequest(new ProblemDetails
                {
                    Title = "Update isVisible of Responces Reading List Fail",
                    Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                    Detail = "User is Not authorizate",
                    Status = 400,

                });
            }
        }

    }
}
