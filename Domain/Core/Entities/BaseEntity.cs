using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public abstract class BaseEntity
    {
        #region protected properties

        protected Guid _id;
        protected Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        #endregion

        #region public properties

        public Guid Id { get => _id; }

        #endregion

    }
}
