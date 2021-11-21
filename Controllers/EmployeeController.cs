using EmpResumeBuilder.Models;
using HiQPdf;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmpResumeBuilder.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        EmpDataContext objDataContext = new EmpDataContext();
        // GET: Demo  
        public ActionResult EmpDetails()
        {

            return View(objDataContext.employees.ToList());
        }
        public ActionResult EnterEmployeeDetails()
        {

            return View();
        }
        [HttpPost]
        public ActionResult EnterEmployeeDetails(Employee objEmp)
        {
            objDataContext.employees.Add(objEmp);
            objDataContext.SaveChanges();
            ViewBag.SuccessMessage = "Data saved successfully !";
            return View();
        }
        public ActionResult PrintPartialViewToPdf(int id)
        {

            Employee employee = objDataContext.employees.FirstOrDefault(c => c.EmpId == id);

            var report = new PartialViewAsPdf("~/Views/Employee/Details.cshtml", employee);
            return report;

        }

        public ActionResult Index()
        {
            var pdfDoc = ExternalCssPdfParser.CreatePdf();
            string mimeType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "inline; filename=" + "CaseReport.pdf");
            return File(pdfDoc, mimeType);
        }
        public ActionResult Details(string id)
        {
            int empId = Convert.ToInt32(id);
            var emp = objDataContext.employees.Find(empId);
            return View(emp);
        }
        


        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                string htmltext = GridHtml;
                htmltext = htmltext.Replace("•", "<br /> •");
                StringReader sr = new StringReader(htmltext);
                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                Document pdfDoc = new Document(PageSize.A4.Rotate(), 15F, 15F, 45F, 0.2F);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                var htmlContext = new HtmlPipelineContext(null);
                htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssResolver.AddCssFile(HttpContext.Server.MapPath("~/Content/Site.css"), true);
                cssResolver.AddCss(".prjtd {white-space: pre-line; }", true);
                IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(pdfDoc, writer)));
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Resume.pdf");
            }

        }

        public ActionResult Edit(string id)
        {
            int empId = Convert.ToInt32(id);
            var emp = objDataContext.employees.Find(empId);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee objEmp)
        {
            var data = objDataContext.employees.Find(objEmp.EmpId);
            if (data != null)
            {
                data.Name = objEmp.Name;
                data.MobileNo = objEmp.MobileNo;
                data.Email = objEmp.Email;
                data.ProjectDetails = objEmp.ProjectDetails;
                data.TechnicalSkills = objEmp.TechnicalSkills;
                data.RelevantExperience = objEmp.RelevantExperience;
            }
            objDataContext.SaveChanges();
            return View();
        }
    }
}