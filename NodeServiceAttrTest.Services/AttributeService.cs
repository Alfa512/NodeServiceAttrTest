using System;
using System.Collections.Generic;
using System.Linq;
using NodeServiceAttrTest.Contracts.Repositories;
using NodeServiceAttrTest.Contracts.Services;
using Microsoft.EntityFrameworkCore;
using NodeServiceAttrTest.Models;
using Attribute = NodeServiceAttrTest.Models.Attribute;

namespace NodeServiceAttrTest.Services
{
    public class AttributeService : IAttributeService
    {
        private IAttributeRepository _repository;

        public AttributeService(IAttributeRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Attribute> GetAll()
        {
            return _repository.GetAll().ToList();
        }


        public Attribute GetById(int id)
        {
            return _repository.GetAll().FirstOrDefault(u => u.Id == id);
        }

        public Attribute Create(Attribute model)
        {
            _repository.Create(model);
            _repository.SaveCganges();
            return model;
        }

        public List<Attribute> CreateRange(List<Attribute> model)
        {
            _repository.CreateRange(model);
            _repository.SaveCganges();
            return model;
        }

        public Attribute Update(Attribute model)
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