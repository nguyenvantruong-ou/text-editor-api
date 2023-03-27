using Microsoft.AspNetCore.Mvc;
using System.Net;
using TextEditor.API.DTOs.Constants;
using TextEditor.API.DTOs.RequestDTOs;
using TextEditor.API.DTOs.ResponseDTOs;
using TextEditor.Domain;
using TextEditor.Domain.Accounts.DomainServices.Interfaces;
using TextEditor.Infrastructure.Datas;
using TextEditor.SharedKernel.MD5;

namespace TextEditor.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHash _MD5Util;
        private readonly IUnitOfWork<TextEditorContext> _ouw;
        private readonly IConfiguration _configuration;
        public AuthController(IAccountService accountService,
                              IHash md5,
                              IUnitOfWork<TextEditorContext> ouw,
                              IConfiguration configuration)
        {
            _accountService = accountService;
            _MD5Util = md5;
            _ouw = ouw;
            _configuration = configuration;
        }

        [HttpPost("auth/register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDTO req)
        {
            try
            {
                if (await _accountService.IsExistCardId(req.IdCard))
                    return BadRequest(new CommonResponseDTO((int)HttpStatusCode.BadRequest, null , "Chứng minh nhân dân đã tồn tại!"));
                await _accountService.RegisterAsync(req.IdCard, _MD5Util.GetHash(req.Password), req.Address, req.Gender, req.Name);
                await _ouw.CompleteAsync();
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, null, Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.InternalServerError,
                                         null, Message.Error, e.Message));
            }
        }

        [HttpPost("auth/login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO req)
        {
            try
            {
                req.Password = _MD5Util.GetHash(req.Password);
                var account = await _accountService.LoginAsync(req.Username, req.Password);
                return Ok(new CommonResponseDTO((int)HttpStatusCode.OK, 
                                                new LoginResponseDTO(account, JwtUtil.GetToken(_configuration,account)), 
                                                Message.Ok));
            }
            catch (Exception e)
            {
                return BadRequest(new CommonResponseDTO((int)HttpStatusCode.InternalServerError,
                                         null, Message.Error, e.Message));
            }
        }
    }
}
