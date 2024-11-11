using BuyerAPI.Dto_s;
using BuyerAPI.Entities;
using BuyerAPI.Entities.DbConnectionContext;
using BuyerAPI.Events;
using BuyerAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;
using Shared.Dto_s;

namespace BuyerAPI.Repositories.Implemantations
{
    public class BuyerHelper : IBuyerHelper
    {
        private static HttpClient client = new HttpClient();
        private string role = nameof(Buyer);

        public async Task<IDataResult<JwtDto>> CheckUser(string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ",""));
            
            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.PostAsJsonAsync("https://localhost:44340/Account/loginAcces", new UserForLoginAccessDto { Role = role });

            if (!request.IsSuccessStatusCode)
            {
                return new ErrorDataResult<JwtDto>(Messages.FailedProccess);
            }

            string response = await request.Content.ReadAsStringAsync();

            JwtDto success = JsonConvert.DeserializeObject<JwtDto>(response);

            if (success is null)
            {
                return new ErrorDataResult<JwtDto>(Messages.FailedProccess);
            }

            return new SuccessDataResult<JwtDto>(success,Messages.SuccessProccess);
        }

        public async Task<BillEvent> CreateABill(CreateBillDTO dto)
        {
            Random random = new Random();

            int billID = random.Next(0, 100);
            int invoiceNum = random.Next(0, 100000);



            using (var context = new BuyerDbContext())
            {
                var newBill = new Bill
                {
                    BillID = billID,
                    BuyerTaxID = dto.BuyerTaxID,
                    SuplierTaxID = dto.SuplierTaxID,
                    InovoiceStatus = Status.New,
                    InvoiceCost = dto.InvoiceCost,
                    InvoiceNumber = invoiceNum.ToString(),
                    TermDate = string.IsNullOrEmpty(dto.TermDate) ? dto.TermDate : DateTime.Now.ToShortDateString()
                };

                var addedEntity = context.Entry(newBill);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();

                return await Task.FromResult(BillEvent.GetViewModel(newBill));

            }

        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBills(string buyerTaxId)
        {
            using (var context = new BuyerDbContext())
            {
                var list = context.Bills.Where(b => b.BuyerTaxID == buyerTaxId).ToList();
                if (list != null)
                {
                    var dtoList = list.Select(BillListingDTO.GetViewModel).ToList();
                    return new SuccessDataResult<List<BillListingDTO>>(dtoList);

                }

                return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);
            }
        }
    }
}
