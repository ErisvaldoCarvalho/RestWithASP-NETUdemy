using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using RestWithASP_NETUdemy.Model;
using RestWithASP_NETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASP_NETUdemy.Repository.Implementations
{
    public class PersonRepositoryImpl : IPersonRepository
    {
        private MySQLContext _context;
        public PersonRepositoryImpl(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null) _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();//.SingleOrDefault(p => p.Id.Equals(id));
        }


        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        private bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

        bool IPersonRepository.Exist(long? id)
        {
            throw new NotImplementedException();
        }
    }
}
