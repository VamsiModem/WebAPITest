using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPITest1.Repository;
using WebAPITest1.Models;

namespace WebAPITest1.Controllers
{
    public class CarsController : ApiController
    {
        // GET: api/Cars
        public IEnumerable<Car> Get()
        {
            CarRepository repo = new CarRepository();
            return repo.GetCars();
        }

        // GET: api/Cars/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Cars
        public HttpResponseMessage Post(Car car)
        {
            CarRepository repo = new CarRepository();
            if (repo.InsertCar(car) == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/Cars/5
        public HttpResponseMessage Put(int id, Car car)
        {
            CarRepository repo = new CarRepository();
            if (repo.UpdateCar(id, car) == -1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return Request.CreateResponse(HttpStatusCode.OK);
            
        }

        // DELETE: api/Cars/5
        public void Delete(int id)
        {
        }
    }
}
