using System.Net;
using AngularMvc6.Services;
using Microsoft.AspNet.Mvc;

namespace AngularMvc6.API
{
    public class StopsController : Controller
    {
        private readonly IStopsService _service;

        public StopsController(IStopsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Stops()
        {
            var serverDataLastModified = _service.GetLastModified();

            if (Request.IsClientDataUpToDate(serverDataLastModified))
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.NotModified);
            }

            Response.SetLatModified(serverDataLastModified);
            return Json(_service.GetStops());
        }
    }
}
