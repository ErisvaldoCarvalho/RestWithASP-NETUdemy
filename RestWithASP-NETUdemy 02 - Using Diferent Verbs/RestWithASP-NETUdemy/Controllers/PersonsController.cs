using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASP_NETUdemy.Model;
using RestWithASP_NETUdemy.Business;

namespace RestWithASP_NETUdemy.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;
        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }


        //GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        //GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        //POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        //PUT api/values
        [HttpPut]
        public IActionResult Put([FromBody]Person person)
        {
            if (person == null) return BadRequest();
            var updatePerson = _personBusiness.Update(person);
            if (updatePerson == null) return NoContent();
            return new ObjectResult(_personBusiness.Update(person));
        }

        //DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}
