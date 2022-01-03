using Core.Enums;
using System;

namespace Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        public ErrorCode ErrorCode { get; set; }

        public BaseException(string message, ErrorCode errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public virtual object GetServerInfoError()
        {
            return new
            {

                ErrorMessage = this.Message,
                ErrorStackTrace = this.StackTrace,
                ErrorCode = (short)this.ErrorCode

            };
        }

    }
    
}
