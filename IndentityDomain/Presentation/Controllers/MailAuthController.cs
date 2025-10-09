using IndentityDomain.Application.DTO;
using IndentityDomain.Application.Enums;
using IndentityDomain.Presentation.UserCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadingListDomain.Application.DTO;

namespace IndentityDomain.Presentation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MailAuthController(ISendAuthCodeCase _sendAuthCodeCase) : ControllerBase
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> StartMailRegister([FromBody]string mail)
        {
            Result eventResult = await _sendAuthCodeCase.Handle(mail, IdentityCommandEnum.Register);

            if (!eventResult.IsSuccess)
            {
                if (eventResult.Error.Equals("User registered")) 
                {
                    return BadRequest();
                }
            }


            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CompleteMailRegister([FromBody] AcceptEmailCodeDTO completeCred)
        {

            return Ok();
        }

        public async Task<IActionResult> StartMailLogin([FromBody] string mail)
        {

            return Ok();
        }
    }
}
