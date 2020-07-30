using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Abstract
{
    public interface IEmailService
    {
        Task SendMail(string subject, string body, string to);

    }
}
