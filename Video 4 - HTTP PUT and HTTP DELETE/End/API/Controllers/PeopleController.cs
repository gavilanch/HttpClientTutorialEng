using Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpClientDemo.API.Controllers
{
    [ApiController]
    [Route("api/people")]
    public class PeopleController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PeopleController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet] 
        public async Task<ActionResult<List<Person>>> Get()
        {
            Console.WriteLine("get");

            return await context.People.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Person person)
        {
            context.Add(person);
            await context.SaveChangesAsync();
            return person.Id;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Person person)
        {
            var personExists = await PersonExists(id);

            if (!personExists)
            {
                return NotFound();
            }

            context.Update(person);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var personExists = await PersonExists(id);

            if (!personExists)
            {
                return NotFound();
            }

            context.Remove(new Person() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> PersonExists(int id)
        {
            return await context.People.AnyAsync(p => p.Id == id);
        }
    }
}
