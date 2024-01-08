global using DoroTech.BookStore.Api;
global using DoroTech.BookStore.Infrastructure;
global using DoroTech.BookStore.Application;
global using DoroTech.BookStore.Contracts.Authentication;
global using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
global using DoroTech.BookStore.Contracts.Responses.Auth;
global using DoroTech.BookStore.Infrastructure.Persistence;
global using DoroTech.BookStore.Api.Infra;
global using DoroTech.BookStore.Application.Common.Interfaces.Services;
global using DoroTech.BookStore.Application.Exceptions;
global using DoroTech.BookStore.Infrastructure.Persistence.Seeds;
global using DoroTech.BookStore.Application.Authentication.Common;
global using DoroTech.BookStore.Domain.Aggregates;
global using DoroTech.BookStore.Contracts;
global using DoroTech.BookStore.Contracts.Requests.Commands;
global using DoroTech.BookStore.Contracts.Requests.Queries;

global using Serilog;
global using Mapster;
global using MapsterMapper;
global using Microsoft.AspNetCore.OData;
global using Microsoft.OpenApi.Models;
global using Microsoft.EntityFrameworkCore;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using OperationResult;
global using ILogger = Serilog.ILogger;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.OData.Query;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using Microsoft.OpenApi.Any;
global using Swashbuckle.AspNetCore.SwaggerGen;


