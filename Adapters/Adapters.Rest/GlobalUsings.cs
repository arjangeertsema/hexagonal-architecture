global using Domain.Abstractions;
global using Domain.Abstractions.UseCases;
global using Common.CQRS.Abstractions;
global using Common.CQRS.Abstractions.Exceptions;
global using Common.UserTasks.Abstractions.UseCases;

global using Common.IAM.Abstractions.Exceptions;

global using Common.DDD.Abstractions.Exceptions;

global using Adapters.Rest.Generated.Models;
global using Adapters.Rest.Filters;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;

global using System.ComponentModel.DataAnnotations;
global using Microsoft.OpenApi.Models;