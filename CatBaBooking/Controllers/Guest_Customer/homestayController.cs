using CatBaBooking.Services.Interfaces.Guest_Customer;
using Microsoft.AspNetCore.Mvc;

namespace CatBaBooking.Controllers.Guest_Customer
{
    public class homestayController : Controller //NamNS
    {
        private readonly IHomestayService _homestayService;

        public homestayController(IHomestayService homestayService)
        {
            _homestayService = homestayService;
        }

        [Route("homestay-page")]
        public IActionResult Index(int page = 1)
        {
            var viewModel = _homestayService.GetHomestays(page);       
            return View("~/Views/Home/Homestay.cshtml", viewModel);
        }

        [Route("homestay/detail/{id}")]
        public IActionResult Detail(int id)
        {
            var viewModel = _homestayService.GetHomestayDetail(id);

            if (viewModel == null)
            {
                return NotFound();
            }
            return View("~/Views/Home/HomestayDetail.cshtml", viewModel);
        }

    }
}
