global using DoroTech.BookStore.Application.Common;
global using DoroTech.BookStore.Application.Repositories;
global using DoroTech.BookStore.Application.RequestHandlers.CommandHandlers;
global using DoroTech.BookStore.Contracts.Requests.Commands.Auth;
global using DoroTech.BookStore.Contracts.Responses.Auth;
global using FluentAssertions;
global using NSubstitute;
global using OperationResult;
global using DoroTech.BookStore.Domain.Entities;
global using MapsterMapper;

global using System.Linq.Expressions;
global using DoroTech.BookStore.Application.Exceptions;
global using DoroTech.BookStore.Contracts;
global using DoroTech.BookStore.Contracts.Requests.Commands;

global using Xunit;