using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models.Entities;
using WebApplication.Repositories;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<Users> _validator;


    public AuthController(IUserRepository userRepository, IValidator<Users> validator)
    {
        _userRepository = userRepository;
        _validator = validator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] Users user)
    {
        var validationResult = await _validator.ValidateAsync(user);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var token = await _userRepository.AuthLogin(user);

        if (token == null)
        {
            return Unauthorized("Invalid username or password");
        }

        return Ok(new { token });
    }
}