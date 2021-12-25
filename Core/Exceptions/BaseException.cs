using System;

namespace Core.Exceptions
{
    public abstract class BaseException : Exception
    {
        
        #region constructor

        protected BaseException(string message) : base(message)
        {
                    
        }

        #endregion
    }
}
