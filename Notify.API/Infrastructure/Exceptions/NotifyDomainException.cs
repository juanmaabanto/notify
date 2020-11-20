using System;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.Exceptions
{
    public class NotifyDomainException : Exception
    {
        private int errorId;

        public int ErrorId => errorId;

        public NotifyDomainException()
        { }

        public NotifyDomainException(string message)
            : base(message)
        { }

        public NotifyDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public NotifyDomainException(string message, int errorId)
            : base(message)
        { 
            this.errorId = errorId;
        }
    }
}