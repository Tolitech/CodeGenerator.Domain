using System;

namespace Tolitech.CodeGenerator.Domain.Services
{
    public interface IUnitOfWorkService : IInfrastructureService
    {
        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}