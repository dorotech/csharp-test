using LibraryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApp.Repositories;
using LibraryApp.Data;
using LibraryApp.Dto.Rent;
using Microsoft.Extensions.Logging;

namespace LibraryApp.Services
{
    public class RentServices
    {
        private LibraryAppContext _context { get; set; }
        private readonly RentRepository rentRepository;
        private readonly BookRepository bookRepository;
        private readonly PersonRepository personRepository;
        public RentServices(LibraryAppContext context)
        {
            _context = context;
            rentRepository = new RentRepository(_context);
            bookRepository = new BookRepository(_context);
            personRepository = new PersonRepository(_context);
        }



        public IEnumerable<RentTb> GetAllRents()
        {
            try
            {
                IEnumerable<RentTb> rents = rentRepository.GetAllRentsRepo();
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public IEnumerable<RentTb> GetAllRentsStatus(string status)
        {
            try
            {
                IEnumerable<RentTb> rents = rentRepository.GetAllRentsStatusRepo(status);
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<RentTb> GetRentsFromRenterCpf(long cpf)
        {
            try
            {
                IEnumerable<RentTb> rents = null;
                rents = rentRepository.GetRentsFromRenterCpfRepo(cpf);
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public IEnumerable<RentTb> GetRentsFromBookId(int id)
        {
            try
            {
                IEnumerable<RentTb> rents = null;
                rents = rentRepository.GetRentsFromBookIdRepo(id);
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public IEnumerable<RentTb> GetRentsFromId(int id)
        {
            try
            {
                IEnumerable<RentTb> rents = null;
                rents = rentRepository.GetRentsFromIdRepo(id);
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> RentBook(RentTbDto rent)
        {
            try
            {
                RentTb rents = null;

                var book = bookRepository.GetBooksFromIdRepo(rent.IdBook);

                if (book.AvailableQuantity <= book.RentedQuantity)
                    throw new Exception("No books available.");
                
                rents = await rentRepository.RentBookRepo(rent);

                book.RentTbs.Add(rents);
                book.RentedQuantity += 1;
                
                var person = personRepository.GetPersonFromCpfRepo(rent.Cpf);
                person.RentTbs.Add(rents);

                await bookRepository.UpdateBookRepo(book);
                await personRepository.UpdatePersonRepo(person);
                
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> ReturnBook(RentTb rent)
        {
            try
            {
                RentTb rents = null;

                rents = await rentRepository.ReturnBookRepo(rent);

                var book = bookRepository.GetBooksFromIdRepo(rent.IdBook);

                book.RentedQuantity += 1;

                await bookRepository.UpdateBookRepo(book);

                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> UpdateRent(RentTb rent)
        {
            try
            {
                RentTb rents = null;
                rents = await rentRepository.UpdateRentRepo(rent);
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public async Task<RentTb> DeleteRent(int id)
        {
            try
            {
                RentTb rent = null;
                rent = await rentRepository.DeleteRentRepo(id);
                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
