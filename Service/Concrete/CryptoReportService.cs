using CryptoReporter.Model;
using CryptoReporter.Service.Contract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CryptoReporter.Service.Concrete
{
    public class CryptoReportService : ICryptoReportService
    {
        private readonly IBinanceService _binanceService;
        private readonly IEmailService _emailService;
        private readonly ITemplateService<CryptoPrice> _templateService;
        public CryptoReportService(IBinanceService binanceService, IEmailService emailService, ITemplateService<CryptoPrice> templateService)
        {
            _binanceService = binanceService;
            _emailService = emailService;
            _templateService = templateService;
        }

        public async Task<bool> SendReport(string symbol)
        {
            var model = await _binanceService.GetPriceAsync(symbol);
            model.Name = "Yasin ilkalp Arabacı";

            string template = "";
            using (StreamReader reader = new StreamReader(Path.GetFullPath("../../../Template/ReportTemplate.html")))
            {
                template = reader.ReadToEnd();
            }

            var htmlBody = await _templateService.CreateMessageContent(template, model);

            var content = new MessageContent("Crypto Reporter By yasinilkalp", htmlBody);

            return await _emailService.Send(content);
        }
    }
}
