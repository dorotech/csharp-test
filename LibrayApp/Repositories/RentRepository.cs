using LibraryApp.Data;
using LibraryApp.Dto.Rent;
using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.Repositories
{
    public class RentRepository
    {
        private readonly LibraryAppContext _context;

        public RentRepository(LibraryAppContext context)
        {
            _context = context;
        }

        public IEnumerable<RentTb> GetAllRentsRepo()
        {
            try
            {
                IEnumerable<RentTb> rents = null;
                rents = (from rent in _context.RentTbs select rent).ToList();
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<RentTb> GetAllRentsStatusRepo(string status)
        {
            try
            {
                IEnumerable<RentTb> rents = null;
                rents = (from rent in _context.RentTbs where rent.Status.Equals(status) select rent).ToList();
                return rents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<RentTb> GetRentsFromRenterCpfRepo(long cpf)
        {
            try
            {
                IEnumerable<RentTb> rent = null;
                rent = (from rents in _context.RentTbs where rents.Cpf.Equals(cpf) select rents).ToList();
                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        public IEnumerable<RentTb> GetRentsFromBookIdRepo(int id)
        {
            try
            {
                IEnumerable<RentTb> rent = null;
                rent = (from rents in _context.RentTbs where rents.IdBook.Equals(id) select rents).ToList();
                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public IEnumerable<RentTb> GetRentsFromIdRepo(int id)
        {
            try
            {
                IEnumerable<RentTb> rent = null;
                rent = (from rents in _context.RentTbs where rents.IdRent.Equals(id) select rents).ToList();
                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> RentBookRepo(RentTbDto rentDto)
        {
            try
            {
                
                RentTb rent = new RentTb
                {
                    IdBook = rentDto.IdBook,
                    Cpf = rentDto.Cpf,
                    RentedDate = rentDto.RentedDate,
                    ReturnDate = rentDto.ReturnDate,
                    Status = "Rented"
                };
                _context.RentTbs.Add(rent);
                await _context.SaveChangesAsync();

                rent = (from rents in _context.RentTbs select rents).OrderByDescending(r=>r.IdRent).FirstOrDefault();

                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> ReturnBookRepo(RentTb rent)
        {
            try
            {
                rent.Status = "Returned";
                _context.Entry(rent).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> UpdateRentRepo(RentTb rent)
        {
            try
            {
                _context.Entry(rent).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        public async Task<RentTb> DeleteRentRepo(int id)
        {
            try
            {
                var rent = (from rents in _context.RentTbs select rents).FirstOrDefault();
                if (rent == null)
                {
                    return null;
                }
                _context.RentTbs.Remove(rent);
                await _context.SaveChangesAsync();

                return rent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
    }
}
