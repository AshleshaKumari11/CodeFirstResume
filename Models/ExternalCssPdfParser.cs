using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.parser;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace EmpResumeBuilder.Models
{
    public class ExternalCssPdfParser
    {
        public static String CSS = "D:/finalproject/New_final_project/itextsharp/EmpResumeBuilder/EmpResumeBuilder/Content/Site.css";
        public static String CSSS = "D:/finalproject/New_final_project/itextsharp/EmpResumeBuilder/EmpResumeBuilder/Content/bootstrap.min.css";
        public static byte[] CreatePdf()
        {
            Document document = new Document(PageSize.A4);
            byte[] result;
            string htmltext = @"
<html><body>
      <div class='container - fluid'>
            <div class='row'>
                <div class='col-sm-3 col-md-6 col-lg-4 prjtd' style='background-color:yellow;'>
                    • Worked in payroll government project for an European country.• Collaborated with Onshore team  for fixing Major/ Critical Client issues.

                </div>
                <div class='col-sm-9 col-md-6 col-lg-8 prjtd' style='background-color:pink;'>
                 • Worked in payroll government project for an European country.• Collaborated with Onshore team  for fixing Major/ Critical Client issues.

                </div>
            </div>
<div class='col - md - 6 color_Blue'>this is div</div></div></body></html>";
            List<string> cssFiles = new List<string>();
            cssFiles.Add(@"/Content/Site.css");
            cssFiles.Add(@"/Content/bootstrap.css");
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                writer.CloseStream = false;
                document.Open();
                HtmlPipelineContext htmlcontext = new HtmlPipelineContext(null);
                htmlcontext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);
                cssFiles.ForEach(i => cssResolver.AddCssFile(HttpContext.Current.Server.MapPath(i), true));
                IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlcontext, new PdfWriterPipeline(document, writer)));
                XMLWorker worker = new XMLWorker(pipeline, true);
                XMLParser xmlParser = new XMLParser(worker);
                xmlParser.Parse(new MemoryStream(Encoding.UTF8.GetBytes(htmltext)));
                document.Close();
                result = stream.GetBuffer();

            }
            return result;

        }
    }
}