using Microsoft.AspNetCore.Mvc;
using AarhusSpaceProgram.Api.DTOs;
using AarhusSpaceProgram.Api.Entities;
using AarhusSpaceProgram.Api.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace AarhusSpaceProgram.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly SpaceProgramContext _context;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    public AccountController(
            SpaceProgramContext context,
            ILogger<AccountController> logger,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    //TODO: maybe insert the correct HTTP request for register
    public async Task<ActionResult> Register(RegisterDTO input)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var newUser = new AppUser
                {
                    UserName = input.Email,
                    Email = input.Email,
                    FullName = input.FullName
                };
                var result = await _userManager.CreateAsync(
                newUser, input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation(
                    "User {userName} ({email}) has been created.",
                    newUser.UserName, newUser.Email);
                    return StatusCode(201,
                    $"User '{newUser.UserName}' has been created.");
                }
                else
                    throw new Exception(
                    string.Format("Error: {0}", string.Join(" ",
                    result.Errors.Select(e => e.Description))));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during user registration");
            return StatusCode(500, ex.Message);
        }
    }

    //TODO: maybe insert the correct HTTP request for login
    public async Task<ActionResult> Login(LoginDTO input)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(input.UserName);
                if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
                    throw new Exception("Invalid login attempt.");
                else
                {
                    var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                    System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
                    SecurityAlgorithms.HmacSha256);
                    var claims = new List<Claim>
                    {
                        new(ClaimTypes.Name, user.UserName)
                    };
                    var jwtObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:Issuer"],
                    audience: _configuration["JWT:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddSeconds(300),
                    signingCredentials: signingCredentials);
                    var jwtString = new JwtSecurityTokenHandler()
                    .WriteToken(jwtObject);
                    return StatusCode(StatusCodes.Status200OK, jwtString);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during user Login");
            return StatusCode(500, ex.Message);
        }
    }
}