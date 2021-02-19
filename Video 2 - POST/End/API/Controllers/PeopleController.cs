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
            return await context.People.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            context.Add(person);
            await context.SaveChangesAsync();
            return person.Id;
        }
    }
}
