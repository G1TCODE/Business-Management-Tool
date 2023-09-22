using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO 1: Cite Milan

namespace BMT.Domain.Shared
{
    public record Money(int Value) 
    {

        #region Operator Overloads

        public static Money operator +(Money a, Money b)
        {
            return new Money(a.Value + b.Value);
        }

        public static Money operator -(Money a, Money b)
        {
            return new Money(a.Value - b.Value);
        }

        public static Money operator *(int b, Money a)
        {
            return new Money(a.Value * b);
        }

        public static bool operator >(Money a, Money b)
        {
            if (a.Value >= b.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator <(Money a, Money b)
        {
            if (a.Value <= b.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static bool operator ==(Money a, Money b)
        //{

        //}

        //public static bool operator !=(Money a, Money b)
        //{

        //}

        #endregion Operator Overloads

    }
}
