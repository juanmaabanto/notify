using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Expertec.Lcc.Services.Notify.API.Services
{
    public sealed class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _context; 

        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int EmpresaId => Convert.ToInt32(_context.HttpContext.User.FindFirst("EmpresaId").Value);

        public string Rol => _context.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

        public string Usuario => _context.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

        public string UsuarioId => _context.HttpContext.User.FindFirst("UsuarioId").Value;
  
    }
}