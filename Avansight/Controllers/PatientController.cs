using Avansight.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avansight.Business_Layer;

namespace Avansight.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }
        public IActionResult Index()
        {
            var getPatients= _patientService.GetPatients();
            return View(getPatients);
        }
    }
}
