using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     Fetches current account balance
        /// </summary>
        /// <returns></returns>
        [Route("default")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> GetAccountBalance()
        {
            var account = await _accountService.GetAccountAsync();

            return Ok(account.Balance);
        }
    }
}
