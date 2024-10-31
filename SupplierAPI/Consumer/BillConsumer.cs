using MassTransit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Entities;
using Shared.Entities.DbConnectionContext;
using Shared.Events;
using Shared.Helpers.Security.JWT;
using SupplierAPI.Extensions;
using System.Security.Claims;

namespace SupplierAPI.Consumer
{
    public class BillConsumer : IConsumer<BillEvent>
    {
        static HttpClient _httpClient = new HttpClient();
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BillConsumer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Consume(ConsumeContext<BillEvent> context)
        {

            using (var dbContext = new SupplyChainDbContext())
            {
                var addedEntity = dbContext.Entry(new QueueMessage
                {
                    QueueGUID = context.MessageId,
                    BuyerTaxID = context.Message.BuyerTaxID,
                    InvoiceCost = context.Message.InvoiceCost,
                    InvoiceNumber = context.Message.InovoiceNumber,
                    SuplierTaxID = context.Message.SuplierTaxID,
                    TermDate = context.Message.TermDate,
                    IsRead = false
                });
                addedEntity.State = EntityState.Added;
                dbContext.SaveChanges();
            }

        }
    }
}
