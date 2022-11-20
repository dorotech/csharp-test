using LibraryApp.Data;
using LibraryApp.Dto.Person;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class PersonRepository
    {
        private readonly LibraryAppContext _context;

        public PersonRepository(LibraryAppContext context)
        {
            _context = context;
        }

        public IEnumerable<PersonTb> GetAllPeopleRepo()
        {
            try
            {
                IEnumerable<PersonTb> people = null;
                people = (from person in _context.PersonTbs select person).ToList();
                foreach (var person in people)
                {
                    var rentList = (from rents in _context.RentTbs where rents.Cpf.Equals(person.Cpf) select rents).ToList();
                    foreach (var rent in rentList)
                    {
                        person.RentTbs.Add(rent);
                    }
                }
                return people.OrderBy(p => p.Name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<PersonTb> GetPersonFromNameRepo(string name)
        {
            try
            {
                IEnumerable<PersonTb> peopleList = null;
                peopleList = (from people in _context.PersonTbs where people.Name.Contains(name) select people).ToList();
                foreach (var person in peopleList)
                {
                    var rentList = (from rents in _context.RentTbs where rents.Cpf.Equals(person.Cpf) select rents).ToList();
                    foreach (var rent in rentList)
                    {
                        person.RentTbs.Add(rent);
                    }
                }
                
                return peopleList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public PersonTb GetPersonFromCpfRepo(long cpf)
        {
            try
            {
                PersonTb person = null;
                person = (from people in _context.PersonTbs where people.Cpf.Equals(cpf) select people).FirstOrDefault();
                var rentList = (from rents in _context.RentTbs where rents.Cpf.Equals(person.Cpf) select rents).ToList();
                foreach (var rent in rentList)
                {
                    person.RentTbs.Add(rent);
                }
                return person;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<PersonTbDto> CreatePersonRepo(PersonTbDto personDto)
        {
            try
            {
                PersonTb person = new PersonTb
                {
                    Name = personDto.Name,
                    Cpf = personDto.Cpf,
                    Email = personDto.Email,
                    Phone = personDto.Phone,
                    Birth = personDto.Birth
                };
                _context.PersonTbs.Add(person);
                await _context.SaveChangesAsync();

                return personDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<PersonTb> UpdatePersonRepo(PersonTb person)
        {
            try
            {
                _context.Entry(person).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return person;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<PersonTb> DeletePersonRepo(int id)
        {
            try
            {
                var person = (from people in _context.PersonTbs select people).FirstOrDefault();
                if (person == null)
                {
                    return null;
                }
                _context.PersonTbs.Remove(person);
                await _context.SaveChangesAsync();

                return person;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
