﻿using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASP_NETUdemy.Model;
using RestWithASP_NETUdemy.Model.Context;
using RestWithASP_NETUdemy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASP_NETUdemy.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private IPersonRepository _repository;
        public PersonBusinessImpl(IPersonRepository repository)
        {
            _repository = repository;
        }

        public Person Create(Person person)
        {
            return _repository.Create(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Person> FindAll()
        {
            return _repository.FindAll();
        }
  
        public Person FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Person Update(Person person)
        {
            return _repository.Update(person);
        }
    }
}
