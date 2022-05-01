using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {

        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(42);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("servererror")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(42);

            var thing = product.ToString();

            return Ok();
        }

        [HttpGet]
        [Route("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet]
        [Route("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }

    }
}
