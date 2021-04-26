using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProductos.Extensions
{
    public static class IntegerExtensions
    {
        public static int ToSumId(this int id)
        {
            
            Func<int, int> calculator = null;
            calculator = (int number) =>
            {
                int sum = 0;
                var numbers = number.ToString().Select(digit => int.Parse(digit.ToString()));
                foreach (var num in numbers)
                {
                    sum += num;
                }
                if (sum >= 10) sum = calculator(sum);
                return sum;
            };
            return int.Parse(string.Concat(id, calculator(id)));
        }
    }
}
