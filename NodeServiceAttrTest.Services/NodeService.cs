using System;
using System.Collections.Generic;
using System.Linq;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Contracts.Services;
using NodeServiceAttrTest.Models;
using Microsoft.EntityFrameworkCore;

namespace NodeServiceAttrTest.Services
{
    public class NodeService : INodeService
    {
        private INodeRepository _repository;

        public NodeService(INodeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Node> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public int Count()
        {
            return _repository.GetAll().Count();
        }


        public Node GetById(int id)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public Node Create(Node model)
        {
            _repository.Create(model);
            _repository.SaveCganges();
            return model;
        }

        public List<Node> CreateRange(List<Node> model)
        {
            _repository.CreateRange(model);
            _repository.SaveCganges();
            return model;
        }

        public Node Update(Node model)
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