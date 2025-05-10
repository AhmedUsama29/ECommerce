using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketNotFoundException(string basketKey) 
                : NotFoundException($"Basket With This Id : {basketKey}  is Not Found")
    {
    }
}
