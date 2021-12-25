using System.Collections.Generic;

namespace Core.Exceptions
{
    public class ValidationEntityException : BaseException
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

        public ValidationEntityException(string message, Dictionary<string, string> errors) : base(message)
        {
            _errors = errors;
        }

        #endregion

    }
}
