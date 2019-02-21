﻿using System;
using System.Collections.Generic;
using System.Linq;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Contracts.Services;
using NodeServiceAttrTest.Models;
using Microsoft.EntityFrameworkCore;
using Attribute = NodeServiceAttrTest.Models.Attribute;

namespace NodeServiceAttrTest.Services
{
    public class ServiceService : IServiceService
    {
        private IServiceRepository _repository;
        private INodeRepository _nodeRepository;
        private IAttributeRepository _attributeRepository;
        private IServiceNodesRepository _serviceNodesRepository;
        private ICustomDataSetRepository _customDataSetRepository;

        public ServiceService(IServiceRepository repository, INodeRepository nodeRepository, IAttributeRepository attributeRepository, IServiceNodesRepository serviceNodesRepository, ICustomDataSetRepository customDataSetRepository)
        {
            _repository = repository;
            _nodeRepository = nodeRepository;
            _attributeRepository = attributeRepository;
            _serviceNodesRepository = serviceNodesRepository;
            _customDataSetRepository = customDataSetRepository;
        }

        public IEnumerable<Service> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public IEnumerable<Models.ViewModels.Services> GetTargetSet(int max = 0)
        {
            var top = max > 0 ? $" TOP({max}) " : " ";
            var sql = @"select distinct " + top + @"s.Id as Id, s.Name as Name
                , n.id as NodeId, n.Name as NodeName, n.ParentId as ParentId, a.Id as AttributeId, a.Name as AttributeName
            FROM[dbo].[Service] as s INNER JOIN[dbo].[ServiceNodes] as sn ON sn.ServiceId = s.Id INNER JOIN[dbo].[UINode] as n ON  sn.NodeId = n.Id
            INNER JOIN[dbo].[Attribute] as a ON a.ServiceId = s.Id";
            var target = _customDataSetRepository.FromSql(sql).ToList();

            var services = target.Select(r => new Models.ViewModels.Services
            {
                Id = r.Id,
                Name = r.Name,
                Attributes = target.Select(t => new Attribute { Id = t.AttributeId, Name = t.AttributeName, ServiceId = t.Id }).Where(a => a.ServiceId == r.Id).Distinct(),
                Nodes = target.Where(t => t.Id == r.Id).Select(n => new Node { Id = n.NodeId, Name = n.NodeName, ParentId = n.ParentId }).Distinct()
            });

            return services;
        }

        public IEnumerable<Models.ViewModels.Services> GetVeryNonOptimizedTargetSet()
        {
            var services = _repository.GetAll().ToList();

            var servicenodes = _serviceNodesRepository.GetAll()
                .Where(r => services.Any(s => s.Id == r.ServiceId)).ToList();

            var nodes = _nodeRepository.GetAll().Where(n => servicenodes.Any(sn => sn.NodeId == n.Id)).ToList();
            var attrs = _attributeRepository.GetAll().ToList();

            var res = services.Select(r => new Models.ViewModels.Services
            {
                Id = r.Id,
                Name = r.Name,
                Nodes = nodes.Where(n => servicenodes.Any(sn => sn.NodeId == n.Id && sn.ServiceId == r.Id)),
                Attributes = attrs.Where(a => a.ServiceId == a.Id)
            });
            return res;
        }

        public Service GetById(int id)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public Service Create(Service model)
        {
            _repository.Create(model);
            _repository.SaveCganges();
            return model;
        }

        public List<Service> CreateRange(List<Service> model)
        {
            _repository.CreateRange(model);
            _repository.SaveCganges();
            return model;
        }

        public ServiceNodes CreateServiceNodeEntry(ServiceNodes model)
        {
            _serviceNodesRepository.Create(model);
            _serviceNodesRepository.SaveCganges();
            return model;
        }

        public IEnumerable<ServiceNodes> CreateServiceNodeEntryRange(List<ServiceNodes> model)
        {
            model = model.GroupBy(nodes => new { nodes.NodeId, nodes.ServiceId }).Distinct().Select(r => new ServiceNodes { NodeId = r.Key.NodeId, ServiceId = r.Key.ServiceId }).ToList();

            _serviceNodesRepository.CreateRange(model);
            _serviceNodesRepository.SaveCganges();
            return model;
        }

        public Service Update(Service model)
        {
            var item = _repository.GetAll().AsNoTracking().FirstOrDefault(u => u.Id == model.Id);

            if (item == null)
                throw new ApplicationException("Task not found");

            _repository.Update(item);
            _repository.SaveCganges();

            return item;
        }

        public void Delete(int id)
        {
            var item = _repository.GetAll().FirstOrDefault(u => u.Id == id);
            if (item != null)
            {
                _repository.Delete(item);
                _repository.SaveCganges();
            }
        }
    }


}