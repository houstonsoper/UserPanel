﻿using Microsoft.AspNetCore.Mvc;
using userpanel.api.Exceptions;

namespace userpanel.api.Middleware;

public class GlobalExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (UserAlreadyExistsException ex)
        {
            await HandleClientErrorAsync(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (InvalidUserCredentialsException ex)
        {
            await HandleClientErrorAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (InvalidPasswordTokenException ex)
        {
            await HandleClientErrorAsync(context, StatusCodes.Status400BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleServerErrorAsync(context, ex);
        }
    }

    private static async Task HandleServerErrorAsync(HttpContext context, Exception ex)
    {
        var traceId = Guid.NewGuid();
            
        //Check if environment is development
        var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            
        var problemDetails = new ProblemDetails
        {
            Title = "Internal Server Error",
            Status = (int)StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path,
            Detail = $"Internal server error occured, traceId : {traceId}",
        };
            
        // Add technical details for developers
        if (isDevelopment)
        {
            problemDetails.Extensions["traceId"] = traceId;
            problemDetails.Extensions["message"] = ex.Message;
            problemDetails.Extensions["stackTrace"] = ex.StackTrace;
        }
        
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(problemDetails);
    }

    private static async Task HandleClientErrorAsync(HttpContext context, int statusCode, string message)
    {
        var response = new { message = message };
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}