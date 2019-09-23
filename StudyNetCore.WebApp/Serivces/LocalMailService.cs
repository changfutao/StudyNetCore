using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StudyNetCore.WebApp.Models;

namespace StudyNetCore.WebApp.Serivces
{
    public interface IMailService
    {
        void Send(string subject, string msg);
    }

    public class LocalMailService: IMailService
    {
        private string _mailTo = "developer@qq.com";
        private string _mailFrom = "noreply@alibaba.com";

        public void Send(string subject,string msg)
        {
            Debug.WriteLine($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }

    public class CloudMailService : IMailService
    {
        private readonly string _mailTo;
        private readonly string _mailFrom;
        private readonly ILogger<CloudMailService> _logger;

        public CloudMailService(
            ILogger<CloudMailService> logger,
            IOptions<mailSettings> mailSettingsOptions
            )
        {
            this._logger = logger;
            this._mailTo = mailSettingsOptions.Value.mailToAddress;
            this._mailFrom = mailSettingsOptions.Value.mailFromAddress;
        }

        public void Send(string subject, string msg)
        {
            _logger.LogInformation($"从{_mailFrom}给{_mailTo}通过{nameof(LocalMailService)}发送了邮件");
        }
    }
}
