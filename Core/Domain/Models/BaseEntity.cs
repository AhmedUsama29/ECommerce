using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; } = default!;
        public string Name { get; set; } = default!;
    }

}
