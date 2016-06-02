using Microsoft.AspNetCore.Mvc;
using RedisCacheWebPoc.Model;
using RedisCacheWebPoc.Services;
using System;
using System.Diagnostics;
using System.Text;

namespace RedisCacheWebPoc.Controllers
{
    public class CacheController : Controller
    {
        private readonly ICacheService _cacheService;

        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public IActionResult Add(int count)
        {
            var response = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                var transaction = new TransactionModel
                {
                    Affiliation = new AffiliationModel { Code = "AffCode", Key = "AffKey" },
                    Amount = 19817,
                    BraspagOrderId = Guid.NewGuid(),
                    Country = "BRA",
                    Currency = "BRL",
                    Customer = new CustomerModel
                    {
                        Name = "Paulo Henrique da Silva Fernandes",
                        Identity = "123.456.789-10"
                    },
                    Installments = 1,
                    MerchantId = Guid.NewGuid(),
                    MerchantOrderId = "201419081119",
                    MustBeProbed = false,
                    PaymentMethodId = Guid.NewGuid(),
                    PaymentPlan = PaymentPlanEnum.Cash,
                    ReceivedDate = DateTime.Now,
                    RequestIp = "127.0.0.1",
                    StartedDate = DateTime.Now,
                    SentOrderId = "201419081123",
                    Status = TransactionStatusEnum.NotFinished,
                    TransactionId = Guid.NewGuid(),
                    TransactionType = TransactionTypeEnum.AutomaticCapture,
                    Card = new CardModel
                    {
                        ExpirationDate = "12/2015",
                        Holder = "Teste T S Testando",
                        Number = "4532117080573703",
                        SecurityCode = "000",
                        Brand = BrandEnum.Visa,
                    },
                    ServiceTaxAmount = 157
                };

                try
                {
                    var timer = new Stopwatch();
                    timer.Start();
                    _cacheService.Add(i.ToString(), transaction);
                    timer.Stop();

                    response.AppendFormat("#{0} added in {1}ms\r\n", i, timer.ElapsedMilliseconds);
                }
                catch (Exception)
                {
                    response.AppendFormat("#{0} error", i);
                }
            }

            return Content(response.ToString());
        }

        public IActionResult AddTransaction()
        {
            var id = Guid.NewGuid();

            var transaction = new TransactionModel
            {
                Affiliation = new AffiliationModel { Code = "AffCode", Key = "AffKey" },
                Amount = 19817,
                BraspagOrderId = Guid.NewGuid(),
                Country = "BRA",
                Currency = "BRL",
                Customer = new CustomerModel
                {
                    Name = "Paulo Henrique da Silva Fernandes",
                    Identity = "123.456.789-10"
                },
                Installments = 1,
                MerchantId = Guid.NewGuid(),
                MerchantOrderId = "201419081119",
                MustBeProbed = false,
                PaymentMethodId = Guid.NewGuid(),
                PaymentPlan = PaymentPlanEnum.Cash,
                ReceivedDate = DateTime.Now,
                RequestIp = "127.0.0.1",
                StartedDate = DateTime.Now,
                SentOrderId = "201419081123",
                Status = TransactionStatusEnum.NotFinished,
                TransactionId = id,
                TransactionType = TransactionTypeEnum.AutomaticCapture,
                Card = new CardModel
                {
                    ExpirationDate = "12/2015",
                    Holder = "Teste T S Testando",
                    Number = "4532117080573703",
                    SecurityCode = "000",
                    Brand = BrandEnum.Visa,
                },
                ServiceTaxAmount = 157
            };

            var timer = new Stopwatch();
            timer.Start();
            _cacheService.Add(id.ToString(), transaction);
            timer.Stop();

            return Created("", new
            {
                Time = timer.ElapsedMilliseconds,
                Id = id
            });
        }

        public IActionResult Get(Guid id)
        {
            var timer = new Stopwatch();
            timer.Start();
            var response = _cacheService.Get<TransactionModel>(id.ToString());
            timer.Stop();

            response.GetTime = timer.ElapsedMilliseconds;

            return Ok(response);
        }
    }
}
