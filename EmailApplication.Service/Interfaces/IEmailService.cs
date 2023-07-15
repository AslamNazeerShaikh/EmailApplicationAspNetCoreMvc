using EmailApplication.Services.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailApplication.Services.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(EmailRequest emailRequest);
    }
}
