using System;
using System.Collections.Generic;
using Tolitech.CodeGenerator.Domain.Repositories;
using Tolitech.CodeGenerator.Domain.Tests.Domain.Entities;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Tests.Domain.Repositories
{
    public interface IPersonRepository : IRepository
    {
        NotificationResult Insert(Person entity);

        IEnumerable<Person> Get();
    }
}