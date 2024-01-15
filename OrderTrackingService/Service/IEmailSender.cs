using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderTrackingService.Service
{
    public interface IEmailSender
    {
        void SendEmail(string toEmail, string subject);
    }
}
