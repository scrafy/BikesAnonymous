using Core.Enums;
using System.Collections.Generic;

namespace Core.Exceptions
{
    public class ValidationException : BaseException
    {
        #region private properties

        private Dictionary<string, string> _errors;

        #endregion

        #region public properties

        public Dictionary<string, string> Errors
        {
            get => _errors ?? new Dictionary<string, string>();
        }

        #endregion

        #region  constructor

        
        public ValidationException(string message, Dictionary<string, string> errors, ErrorCode errorCode) : base(message, errorCode)
        {
            _errors = errors;
        }


        public override object GetServerInfoError()
        {
            return new
            {

                ErrorMessage = this.Message,
                ErrorStackTrace = this.StackTrace,
                ErrorCode = (short)this.ErrorCode,
                ValidationModelErrors = this._errors

            };
        }

        #endregion

    }
}
