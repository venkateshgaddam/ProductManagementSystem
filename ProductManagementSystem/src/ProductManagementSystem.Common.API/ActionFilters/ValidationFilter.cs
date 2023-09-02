﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductManagement.Common.Models;

namespace ProductManagementSystem.Common.API.ActionFilters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                                    .Where(a => a.Value.Errors.Count > 0)
                                    .ToDictionary(k => k.Key, k => k.Value.Errors.Select(a => a.ErrorMessage))
                                    .ToArray();
                var errorResponse = new ErrorResponse();

                foreach (var error in errors)
                {
                    foreach (var item in error.Value)
                    {
                        var errorresponeModel = new ErrorModel { FieldName = error.Key, Message = item };
                        errorResponse.Errors.Add(errorresponeModel);
                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
