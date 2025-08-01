using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TopicDomain.Application.DTO;
using TopicDomain.Application.UseCases;
using TopicDomain.Application.UseCases.CreateTopic;
using TopicDomain.Domain;

namespace TopicDomain.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ApiController(
            ICreateTopicCase _createTopicCase,
            FindTopicsByNameCase _findTopicsByNameCase,
            FindTopicsDTOByNameCase _findTopicsDTOByNameCase,
            GetLast100TopicsCase _getLast100TopicsCase,
            GetTopicCase _getTopicCase,
            GetTopicDTOByIdCase _getTopicDTOByIdCase,
            GetTopicsByIdsCase _getTopicsByIdsCase,
            GetTopicDTOByIdsCase _getTopicDTOByIdsCase
        ) : ControllerBase
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTopic([FromBody]Topic topic)
        {
            var result = await _createTopicCase.HandleAsync(topic);
            if (result.IsSuccess) 
            {
                return Created();
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Topic created fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FindTopicsByName(string name)
        {
            var result = await _findTopicsByNameCase.HandleAsync(name);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Find Topic by name fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> GetTopic(string Id) 
        {
            var result = await _getTopicCase.HandleAsync(Id);
            if (result.IsSuccess) {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Get topic by Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetTopicsByIds([FromBody]List<string> Ids) 
        {
            var result = await _getTopicsByIdsCase.HandleAsync(Ids);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Get last 100 topic Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FindTopicDTOsByName(string Name) 
        {
            var result = await _findTopicsDTOByNameCase.HandleAsync(Name);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Find TopicDTO By Name Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLast100Topics() {
            var result = await _getLast100TopicsCase.HandleAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Get last 100 topic Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetTopicDTOByIds([FromBody] List<string> ids) 
        {
            var result = await _getTopicDTOByIdsCase.HandleAsync(ids);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Get last 100 topic Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetTopicDTOById(string Id) 
        {
            var result = await _getTopicDTOByIdCase.HandleAsync(Id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(new ProblemDetails
            {
                Title = "Get topicDTO by id Fail",
                Type = "https://developer.mozilla.org/ru/docs/Web/HTTP/Reference/Status/400",
                Detail = result.Error,
                Status = 400,
            });
        }





    }
}
