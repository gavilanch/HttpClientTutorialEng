using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PeopleController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            return NotFound();
            return new List<Person>() { new Person() { Id = 1, Name = "Felipe"},
            new Person(){Id = 2, Name = "Claudia"}};
            //return await context.People.ToListAsync();
        }
    }
}
