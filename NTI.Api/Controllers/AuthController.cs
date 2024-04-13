using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NTI.Application.InputModels.Employee;
using NTI.Application.InputModels.Login;
using NTI.Application.Interfaces.Services;

namespace NTI.Api.Controllers
{
    /// <summary>
    /// Controller for employee to get registered and login
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Constructor for the AuthController
        /// </summary>
        /// <param name="authenticationService"></param>
        /// <param name="employeeService"></param>
        public AuthController(IAuthenticationService authenticationService, IEmployeeService employeeService)
        {
            _authenticationService = authenticationService;
            _employeeService = employeeService;
        }

        /// <summary>
        /// Register a new employee to be able to login
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] EmployeeInputModel inputModel)
        {
            var result = await _employeeService.CreateAsync(inputModel);
            if (result.IsSuccessfulWithNoErrors)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Login for a created employee
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputRecord inputModel)
        {
            var result = await _authenticationService.AuthenticateAsync(inputModel.Email, inputModel.Password!);
            if (result.IsSuccessfulWithNoErrors)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }


    }
}