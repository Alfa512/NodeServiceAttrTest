﻿using System.Collections.Generic;
using NodeServiceAttrTest.Models;

namespace NodeServiceAttrTest.Contracts.Services
{
    public interface IServiceService
    {
        IEnumerable<Service> GetAll();
        IEnumerable<Service> GetTop(int top);
        IEnumerable<Models.ViewModels.Services> GetTargetSet(int max = 0);
        IEnumerable<Models.ViewModels.Services> GetNonOptimizedTargetSet(int max = 0);
        Service GetById(int id);
        Service Create(Service item);
        List<Service> CreateRange(List<Service> model);
        ServiceNodes CreateServiceNodeEntry(ServiceNodes model);
        IEnumerable<ServiceNodes> CreateServiceNodeEntryRange(List<ServiceNodes> model);
        Service Update(Service item);
        void Delete(int id);
    }
}
