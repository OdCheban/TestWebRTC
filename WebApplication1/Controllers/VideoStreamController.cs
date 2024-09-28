using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    public class VideoStreamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
