using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoElastic.ElasticSearch.Agenda;
using DemoElastic.EntityFramework;
using DemoElastic.Model.Agenda;
using Microsoft.AspNetCore.Mvc;

namespace DemoElastic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        CLHContext _context;

        public AgendaController(CLHContext context)
        {
            _context = context;
        }

        // GET: api/Agenda
        /// <summary>
        /// Get ALL contacts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<AgendaContatos2> Get()
        {
            return _context.AgendaContatos2.ToList();
        }

        // GET: api/Agenda/Index/5
        /// <summary>
        /// Index contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Index")]
        public async Task<string> Index(int id)
        {
            try
            {
                AgendaContatos2 agenda;
                agenda = _context.AgendaContatos2.Where(a => a.Id.Equals(id)).FirstOrDefault();
                
                AgendaQuery aq = new AgendaQuery();
                //await aq.CreateIndexAsync();
                await aq.IndexAsync(agenda);
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Reset index
        /// </summary>
        /// <returns></returns>
        [HttpGet("RestartIndex")]
        public async Task<string> RestartIndex()
        {
            try
            {
                AgendaQuery aq = new AgendaQuery();
                await aq.CreateIndexAsync();
                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


    }
}
