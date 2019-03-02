using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NodeServiceAttrTest.Contracts.Services;
using NodeServiceAttrTest.Models;
using Attribute = NodeServiceAttrTest.Models.Attribute;

namespace NodeServiceAttrTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private INodeService _nodeService;
        private IServiceService _serviceService;
        private IAttributeService _attributeService;

        public ValuesController(INodeService nodeService, IServiceService serviceService, IAttributeService attributeService)
        {
            _nodeService = nodeService;
            _serviceService = serviceService;
            _attributeService = attributeService;
        }


        // GET api/values/generate
        [Route("generate")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Generate()
        {
            var rand = new Random();
            /*
            var nodes = new List<Node>();

            for (var i = 1; i < 401; i++)
            {
                nodes.Add(new Node
                {
                    ParentId = i,
                    Name = "Node " + (i-1)
                });
            }

            _nodeService.CreateRange(nodes);
            */

            /*var services = new List<Service>();
            for (var i = 0; i < 10000; i++)
            {
                services.Add(new Service
                {
                    Name = "Service " + i
                });
            }
            _serviceService.CreateRange(services);


            var attributes = new List<Attribute>();
            for (var i = 0; i <= 20001; i++)
            {
                attributes.Add(new Attribute
                {
                    Name = "Attribute " + i,
                    ServiceId = rand.Next(1, 10001)
                });
            }
            _attributeService.CreateRange(attributes);

            var entry = new ServiceNodes()
            {
                ServiceId = rand.Next(1, 10002),
                NodeId = rand.Next(6, 405)
            };
            var entryList = new List<ServiceNodes>();
            for (var i = 1; i <= 40001; i++)
            {
                entryList.Add(entry);
                entry = new ServiceNodes
                {
                    NodeId = rand.Next(6, 405),
                    ServiceId = rand.Next(1, 10002)
                };
            }

            _serviceService.CreateServiceNodeEntryRange(entryList);*/

            return new string[] { "Nodes: " + _nodeService.Count(), "value2" };
        }

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            var watch = new Stopwatch();
            watch.Start();
            var services = _serviceService.GetTargetSet();
            var res = services.ToArray();
            watch.Stop();
            var el = watch.ElapsedMilliseconds;
            return $"ResponseTime: {el} ms," + JsonConvert.SerializeObject(res);
        }


        // GET api/values/top/100
        [Route("top/{top}")]
        [HttpGet]
        public ActionResult<string> GetTop(int top = 0)
        {
            var watch = new Stopwatch();
            watch.Start();
            var services = _serviceService.GetTargetSet(top);
            var res = services.ToArray();
            watch.Stop();
            var el = watch.ElapsedMilliseconds;
            return $"ResponseTime: {el} ms," + JsonConvert.SerializeObject(res);
        }

        // GET api/values/slow
        [Route("slow")]
        [HttpGet]
        public ActionResult<string> GetSlow()
        {
            var services = _serviceService.GetNonOptimizedTargetSet();
            var res = services.ToArray();
            return JsonConvert.SerializeObject(res);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
