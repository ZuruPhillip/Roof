using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;
using Volo.Abp.Validation;
using ZURU.Roof.Extensions;

namespace ZURU.Roof.Web.Filter
{

    public class CustomAbpExceptionFilter : AbpExceptionFilter
    {
        protected override async Task HandleAndWrapException(ExceptionContext context)
        {
            //TODO: Trigger an AbpExceptionHandled event or something like that.
            await context.GetRequiredService<IExceptionNotifier>().NotifyAsync(new ExceptionNotificationContext(context.Exception));

            if (context.Exception is AbpValidationException)
            {
                /*
                 *  如果是参数验证异常，显示详细的错误信息
                 */

                var ex = (AbpValidationException)context.Exception;

                //默认只显示2个参数验证信息，太多的就不显示
                var exMsg = string.Join("; ", ex.ValidationErrors.Skip(0).Take(2).Select(p => p.ErrorMessage));

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Result = new ObjectResult(
                    new WrapResult(false, message: exMsg).SetHttpCode(HttpStatusCode.BadRequest)
                    );
            }
            else
            {
                //自定义返回结果:Http状态码还是200，但是自定义的Code固定为500
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Result = new ObjectResult(new WrapResult(false, message: context.Exception.Message));
            }
            context.Exception = null; //Handled!
        }

        protected override void LogException(ExceptionContext context, out RemoteServiceErrorInfo remoteServiceErrorInfo)
        {
            var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
            var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
            remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
            {
                options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
                options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
            });

            var config = context.GetRequiredService<IConfiguration>();

            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine($"--------[{config["ServiceNo"]}] -- {nameof(RemoteServiceErrorInfo)} ----------");
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<AbpExceptionFilter>>(NullLogger<AbpExceptionFilter>.Instance)!;
            var logLevel = context.Exception.GetLogLevel();
            logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());

            var err = new Exception($"[{config["ServiceNo"]}]异常:{context.Exception.Message}", context.Exception);
            logger.LogException(err, logLevel);
        }
    }
}
