using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Application.DTO;
using System.Security.Claims;
using MyAPI.CrossCutting.ExtensionMethods;


namespace MyAPI.Controllers
{
    public abstract class BaseController : Controller
    {
        public IActionResult BaseResponse<T>(BaseResponse<T> response)
        {
            if (response.Errors != null && response.Errors.Count > 0)
                return BadRequest(response);
            else
                return Ok(response);
        }

        protected IActionResult ResponseBadRequest<T>(string message)
        {
            var errors = new List<ErrorResponse>();

            errors.Add(new ErrorResponse { ErrorMessage = message });

            return BadRequest(new BaseResponse<T>
            {
                ValidationsErrors = errors
            });
        }

        protected IActionResult ResponseBadRequest<T>(IEnumerable<string> messages)
        {
            var errors = new List<ErrorResponse>();

            messages.ToList().ForEach(m => errors.Add(new ErrorResponse { ErrorMessage = m }));

            return BadRequest(new BaseResponse<T>
            {
                ValidationsErrors = errors
            });
        }

        protected IEnumerable<string> GetModelStateErrors()
        {
            var errorsMsgs = new List<string>();

            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;

                errorsMsgs.Add(erroMsg);
            }

            return errorsMsgs;
        }

        public long? GetDashboardUserId()
        {
            return User.GetUserId();
        }

        protected string GetUserUId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            return identity.Claims.Where(x => x.Type == "user_id").FirstOrDefault()?.Value;
        }
    }
}
