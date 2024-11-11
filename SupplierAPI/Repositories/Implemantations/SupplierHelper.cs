using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Constant;
using Shared.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using SupplierAPI.Dto_s;
using SupplierAPI.Entities;
using SupplierAPI.Entities.DbConectionContext;
using SupplierAPI.Events;
using SupplierAPI.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace SupplierAPI.Repositories.Implemantations
{
    public class SupplierHelper : ISupplierHelper
    {
        private static HttpClient client = new HttpClient();
        private string role = nameof(Supplier);

        public async Task<IDataResult<JwtDto>> CheckUser(string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ",""));

            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.PostAsJsonAsync("https://localhost:44340/Account/loginAcces", new UserForLoginAccessDto { Role = role});

            if (!request.IsSuccessStatusCode)
            {
                return new ErrorDataResult<JwtDto>(Messages.FailedProccess);
            }

            var response = await request.Content.ReadAsStringAsync();

            JwtDto success = JsonConvert.DeserializeObject<JwtDto>(response);
            
            if (success is null)
            {
                return new ErrorDataResult<JwtDto>(Messages.FailedProccess);
            }

            return new SuccessDataResult<JwtDto>(success,Messages.SuccessProccess);
        }

        public async Task<IDataResult<EarlyPaymentEvent>> CreateAEarlyTask(string invoiceNumber)
        {
            using (var context = new SuplierDbContext())
            {
                var data = context.Bills.FirstOrDefault(b => b.InvoiceNumber == invoiceNumber);

                if (data == null)
                {
                    return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
                }

                data.InovoiceStatus = Status.Usage;

                var updatedEntity = context.Entry(data);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();

                var request = await client.GetFromJsonAsync<bool>("https://localhost:7007/Financial/earlypaymentrequest?invoiceNumber=" + invoiceNumber);

                if (!request)
                {
                    return new ErrorDataResult<EarlyPaymentEvent>(Messages.FailedProccess);
                }

                return new SuccessDataResult<EarlyPaymentEvent>(EarlyPaymentEvent.GetViewModel(data), Messages.SuccessProccess);
            }



        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBillswithSupplier(string supplierTaxId)
        {
            

            if (supplierTaxId is not null)
            {
                using (var context = new SuplierDbContext())
                {
                    var list = context.Bills.Where(s => s.SuplierTaxID == supplierTaxId).ToList();

                    return new SuccessDataResult<List<BillListingDTO>>(list.Select(BillListingDTO.GetViewModel).ToList(), Messages.SuccessProccess);

                }

            }
            return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);


        }
    }
}
