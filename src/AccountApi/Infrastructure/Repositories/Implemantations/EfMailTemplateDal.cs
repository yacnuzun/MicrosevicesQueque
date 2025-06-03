using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Data;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Shared.Persistance.Implamantations;
using System;

namespace AccountApi.Infrastructure.Repositories.Implemantations
{
    public class EfMailTemplateDal : EfRepository<EmailTemplate, AccountDbContext>, IEfMailTemplateDal
    {
        public EfMailTemplateDal(AccountDbContext context) : base(context)
        {
        }
    }
}
