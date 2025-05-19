using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class DeliveryMethodNotFoundException(int id) 
                    : NotFoundException($"Delivery Method With This Id : {id} is Not Found")
    {
    }
}
