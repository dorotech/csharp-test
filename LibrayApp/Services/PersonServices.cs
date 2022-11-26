using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Repositories;
using LibraryApp.Data;
using LibraryApp.Dto.Person;
using Microsoft.Extensions.Logging;

namespace LibraryApp.Services
{
    public class PersonServices
    {
        private LibraryAppContext _context { get; set; }
        private readonly PersonRepository personRepository;
        public PersonServices(LibraryAppContext context)
        {
            _context = context;
            personRepository = new PersonRepository(_context);
        }



        public IEnumerable<PersonTb> GetAllPeople()
        {
            try
            {
                IEnumerable<PersonTb> people = personRepository.GetAllPeopleRepo();
                return people;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<PersonTb> GetPersonFromName(string name)
        {
            try
            {
                IEnumerable<PersonTb> people = null;
                people = personRepository.GetPersonFromNameRepo(name);
                return people;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public PersonTb GetPersonFromCpf(long cpf)
        {
            try
            {
                PersonTb people = null;
                people = personRepository.GetPersonFromCpfRepo(cpf);
                return people;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<PersonTbDto> CreatePerson (PersonTbDto person)
        {
            try
            {
                PersonTbDto people = null;
                people = await personRepository.CreatePersonRepo(person);
                return people;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<PersonTb> UpdatePerson(PersonTb person)
        {
            try
            {
                PersonTb people = null;
                people = await personRepository.UpdatePersonRepo(person);
                return people;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public async Task<PersonTb> DeletePerson(int id)
        {
            try
            {
                PersonTb person = null;
                person = await personRepository.DeletePersonRepo(id);
                return person;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
