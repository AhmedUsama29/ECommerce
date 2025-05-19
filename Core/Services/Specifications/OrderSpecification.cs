using Domain.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecification : BaseSpecifications<Order>
    {

        // Get By Id
        public OrderSpecification(Guid id) : base(O => O.Id == id)
        {

            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);

        }

        //Get All
        public OrderSpecification(string email) : base(O => O.UserEmail == email)
        {

            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);

        }

    }
}
