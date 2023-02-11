using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.data;

namespace SignalR.Controllers
{
    [ApiController]
    [Route("api/calls")]
    public class CallsController : Controller
    {
        private readonly SRDB_Context _ctx;

        public CallsController(SRDB_Context ctx)
        {
            _ctx = ctx;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var calls = await _ctx.calls.ToListAsync();

            return Ok(calls);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var call = await _ctx.calls.Where(c => c.Id == id).FirstOrDefaultAsync();
                if (call == null) return BadRequest();

                _ctx.Remove(call);
                if (await _ctx.SaveChangesAsync() > 0)
                {
                    return Ok(new { success = true });
                }
                else
                {
                    return BadRequest("Database Error");
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
