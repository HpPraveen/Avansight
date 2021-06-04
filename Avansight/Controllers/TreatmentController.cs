using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avansight.Business_Layer;

namespace Avansight.Controllers
{
    public class TreatmentController : Controller
    {
        private readonly TreatmentService _treatmentService;

        public TreatmentController(TreatmentService treatmentService)
        {
            _treatmentService = treatmentService;
        }
        public IActionResult Index()
        {
            return View(null);
        }
    }
}
