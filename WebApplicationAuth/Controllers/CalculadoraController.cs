using System.Web.Http;
using System.Web.Http.Description;
using WebApplicationAuth.Models;

namespace WebApplicationAuth.Controllers
{
    [Authorize]
    public class CalculadoraController : ApiController
    {
        [ResponseType(typeof (int))]
        [HttpPost]
        [Route("api/suma")]
        public IHttpActionResult Suma(Operacion op) => Ok(op.Op1 + op.Op2);

        [ResponseType(typeof(int))]
        [HttpPost]
        [Route("api/resta")]
        public IHttpActionResult Resta(Operacion op) => Ok(op.Op1 - op.Op2);

        [ResponseType(typeof(int))]
        [HttpPost]
        [Route("api/producto")]
        public IHttpActionResult Producto(Operacion op) => Ok(op.Op1 * op.Op2);

        [ResponseType(typeof(int))]
        [HttpPost]
        [Route("api/division")]
        public IHttpActionResult Division(Operacion op) => Ok(op.Op1 / op.Op2);
    }
}
