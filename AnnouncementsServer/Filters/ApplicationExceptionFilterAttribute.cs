﻿using AnnouncementsServer.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace AnnouncementsServer.Filters
{
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            if (context.Exception is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new { errorMessage = context.Exception.Message });
            }
            else if(context.Exception is NotValidException)
            {
                context.Result = new BadRequestObjectResult(new { errorMessage = context.Exception.Message });

            }
        }
    }
}
