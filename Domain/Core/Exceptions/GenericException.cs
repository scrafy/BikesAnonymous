using Core.Enums;

namespace Core.Exceptions
{
    public class GenericException : BaseException
    {

        #region  constructor

        public GenericException(string message, ErrorCode errorCode) : base(message, errorCode)
        {
            
        }

        #endregion

    }
}
