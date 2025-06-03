using AccountApi.Domain.Entities;
using Shared.Persistance.Interfaces;

namespace AccountApi.Infrastructure.Repositories.Interfaces
{
    public interface IEfMailTemplateDal : IRepository<EmailTemplate> 
    { 
    }
}
