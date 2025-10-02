using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PAW3.Data.DTOs;
using PAW3.Data.Models;

namespace PAW3.Data.People
{
    public class PeopleData
    {
        private readonly TestdbContext _context;

        public PeopleData(TestdbContext context) { 
        
            _context = context;
        
        }

        public async Task<IEnumerable<PersonDTO>> GetDataPeopleAsync() { 
        
            return await _context.People.ToListAsync();
        
        }
    }
}
