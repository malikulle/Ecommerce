using System;
using System.Collections.Generic;
using System.Text;
using static System.DateTime;

namespace ECommerce.Entity.Trackable
{
    public class BaseEntity
    {
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public BaseEntity()
        {
            IsActive = true;
            CreatedDate = DateTime.Now;
        }
    }
}
