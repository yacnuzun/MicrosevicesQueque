﻿using BuyerAPI.Dto_s;
using BuyerAPI.Entities;
using BuyerAPI.Repositories.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Constant;
using Shared.Dto_s;
using Shared.Events;
using Shared.Helpers.ResponseModels.GenericResultModels;
using System.Net.Http.Headers;

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

        public async Task<BillEvent> CreateABill(CreateBillDTO dto, string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.PostAsJsonAsync("https://localhost:7221/Bill/createabill", dto);

            if (!request.IsSuccessStatusCode)
            {
                return null;
            }

            string response = await request.Content.ReadAsStringAsync();

            BillResponseDto dtoResponse = JsonConvert.DeserializeObject<BillResponseDto>(response);

            if (dtoResponse is null)
            {
                return null;
            }

            return await Task.FromResult(BillEvent.GetViewModel(dtoResponse));
        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBills(string buyerTaxId, string token)
        {
            var headerAutho = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));

            client.DefaultRequestHeaders.Authorization = headerAutho;

            var request = await client.GetFromJsonAsync<List<BillListingDTO>>("https://localhost:7221/bill/getbillbuyer?buyerTaxId=" + buyerTaxId);

            //var response = JsonConvert.DeserializeObject<IDataResult<List<BillListingDTO>>>(request);

            if (request is null)
            {
                return new ErrorDataResult<List<BillListingDTO>>();
            }

            return new SuccessDataResult<List<BillListingDTO>>(request);
            
        }
    }
}
