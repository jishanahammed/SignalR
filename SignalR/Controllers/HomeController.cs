using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.data;
using SignalR.Hubs;
using SignalR.Models;
using System.Diagnostics;

namespace SignalR.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SRDB_Context _context;
        private readonly IHubContext<callCenterHub> _hubContext;

        public HomeController(ILogger<HomeController> logger, SRDB_Context context, IHubContext<callCenterHub> hubContext)
        {
            _logger = logger;
            _context = context;
            _hubContext = hubContext;   
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(call model)
        {
            if (ModelState.IsValid) {
                _context.Add(model);
                if (await _context.SaveChangesAsync()>0)
                {
                    await _hubContext.Clients.All.SendAsync("newCallReceived", model);
                    ModelState.Clear();
                }


            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Calls()
        {
            return View();
        }


    }
}