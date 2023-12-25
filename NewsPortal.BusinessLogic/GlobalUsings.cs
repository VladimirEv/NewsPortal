// Global using directives

global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
//global using NewsPortal.Claims;
global using NewsPortal.Domain.Entities;
global using NewsPortal.Infrastucture.Common.Response.Interfaces;
global using NewsPortal.Infrastucture.Common.Response.ResponseModels.Base;
global using NewsPortal.Infrastucture.Constants;
global using NewsPortal.Infrastucture.Extensions;
global using NewsPortal.BusinessLogic.Constants;
global using NewsPortal.BusinessLogic.Interfaces;
global using NewsPortal.BusinessLogic.Models;
global using NewsPortal.BusinessLogic.Models.RequestModels;
global using NewsPortal.BusinessLogic.Services;
global using NewsPortal.Persistance;
global using AutoMapper;
global using FluentValidation;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using Serilog;
global using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
