using buildingBlocksCore.Models.Response;
using buildingBlocksCore.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace buildingBlocksServices.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly LNotifications _notifications;

        public MainController(LNotifications notifications)
        {
            _notifications = notifications;

        }
        [NonAction]
        public bool IsValid()
        {
            return !_notifications.Any();
        }
        [NonAction]
        protected void ClearErrors()
        {
            _notifications.Clear();
        }

        [NonAction]
        protected IActionResult ReturnModelState(ModelStateDictionary modelState)
        {

            NotifyModelStateErrors();

            return Response(null);

        }
        [NonAction]
        protected async Task<IActionResult> ExecControllerAsync<T>
                            (Func<Task<T>> func)
        {
            try
            {
                return Response(await func());
            }
            catch (Exception ex)
            {

                AddError(ex);
                return Response(null);
            }
        }

        [NonAction]
        protected async Task<IActionResult> ExecControllerApiGatewayAsync<T>
                    (Func<Task<BaseResponseApi<T>>> func)
        {
            try
            {
                return Response(await func());
            }
            catch (Exception ex)
            {

                AddError(ex);
                return Response(null);
            }
        }

        [NonAction]
        protected async Task<IActionResult> ExecControllerAsync(Func<Task> func)
        {
            try
            {
                await func.Invoke();
                return Response(null);
            }
            catch (Exception ex)
            {

                AddError(ex);
                return Response(null);
            }
        }




        //
        [NonAction]
        protected IActionResult ExecController(object result = null)
        {
            try
            {
                return Response(result);
            }
            catch (Exception ex)
            {

                AddError(ex);
                return Response(null);
            }
        }


        [NonAction]
        protected new IActionResult Response(object result = null)
        {
            if (IsValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            if (_notifications.Any(x => x.TypeNotificationNoty == TypeNotificationNoty.BreakSystem))
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(new
                    {
                        success = false,
                        data = result,
                        errors = _notifications
                    })
                };
            }
            return BadRequest(new
            {
                success = false,
                data = result,
                errors = _notifications
            });
        }


        [NonAction]
        protected new IActionResult Response<T>(BaseResponseApi<T> result = null)
        {
            if (IsValid())
            {
                return Ok(result);
            }

            if (_notifications.Any(x => x.TypeNotificationNoty == TypeNotificationNoty.BreakSystem))
            {
                return new ContentResult()
                {
                    StatusCode = 500,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(result)
                };
            }
            return BadRequest(result);
        }

        [NonAction]
        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AddError(new LNotification { Message = erroMsg });

            }
        }
        [NonAction]
        protected void AddError(IdentityResult result)
        {
            foreach (var error in result.Errors)
                AddError(new LNotification { Message = error.Description });
        }

        [NonAction]
        protected void AddError(Exception except)
        {
            if (except is DomainException)
            {
                _notifications.Add(new LNotification { Message = except.Message });
                return;
            }
            _notifications.Add(new LNotification { TypeNotificationNoty = TypeNotificationNoty.BreakSystem, Message = except.Message });
        }

        [NonAction]
        protected void AddError(LNotification erro)
        {
            _notifications.Add(erro);
        }

    }

}
