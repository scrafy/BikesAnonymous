using Core.Entities;
using DataLayer.Enums;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace DataLayer.Comparers
{
    public class CyclistComparer : IEqualityComparer<Cyclist>
    {
        private COMPARERBY _comparerBy;

        public CyclistComparer(COMPARERBY comparerBy)
        {
            _comparerBy = comparerBy;
        }

        public bool Equals([AllowNull] Cyclist x, [AllowNull] Cyclist y)
        {
            if (_comparerBy == COMPARERBY.EMAIL)
                return CompareByEmail(x, y);

            if (_comparerBy == COMPARERBY.USERNAME)
                return CompareByUsername(x, y);

            return false;
        }
    

        private bool CompareByUsername([AllowNull] Cyclist x, [AllowNull] Cyclist y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Username == y.Username)
                return true;
            else
                return false;
        }

        private bool CompareByEmail([AllowNull] Cyclist x, [AllowNull] Cyclist y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else if (x.Email == y.Email)
                return true;
            else
                return false;
        }

        public int GetHashCode([DisallowNull] Cyclist obj)
        {
            return 1;
        }
    }
}
