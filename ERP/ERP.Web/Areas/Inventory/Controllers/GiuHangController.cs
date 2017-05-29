
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Web.Areas.Inventory.Controllers
{
    public class GiuHangController : Controller
    {
        // GET: Inventory/GiuHang
        public ActionResult Index()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateInput(false)]
        //public FileResult Export(string GridHtml)
        //{
        //    using (MemoryStream stream = new System.IO.MemoryStream())
        //    {
        //        //Step 1: Create a System.IO.FileStream object:
        //            FileStream fs = new FileStream("Quotation.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
        //        //Step 2: Create a iTextSharp.text.Document object:
        //            Document doc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //        //Step 3: Create a iTextSharp.text.pdf.PdfWriter object.It helps to write the Document to the Specified FileStream:
        //            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
        //        //Step 4: Openning the Document:
        //            doc.Open();
        //        //Step 5: Adding a Paragraph by creating a iTextSharp.text.Paragraph object:
        //            doc.Add(new Paragraph("Hello World"));
        //        //Step 6: Closing the Document:
        //            doc.Close();



        //        //StringReader sr = new StringReader(GridHtml);
        //        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
        //        //PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
        //        //pdfDoc.Open();
        //        //XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
        //        //pdfDoc.Close();
        //        return File(stream.ToArray(), "application/pdf", "Grid.pdf");
        //    }
        //}

    }
}