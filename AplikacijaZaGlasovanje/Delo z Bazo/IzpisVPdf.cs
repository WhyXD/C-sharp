using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Net;
using AplikacijaGlasovanje.ModelsUpdate;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Microsoft.Extensions.Configuration;

namespace AplikacijaGlasovanje.Delo_z_Bazo
{
    public  class IzpisVPdf
    {
        private VoteContext _voteContext;
        private DeloZBazo _dzb;
        public IzpisVPdf(VoteContext Contex, DeloZBazo dzb)
        {
            _voteContext = Contex;
            _dzb = dzb;
        }     
        public  FileStream izpisi()
        { 
            /* FileStream fs = File.Create(path);
             Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
             PdfWriter writer = PdfWriter.GetInstance(doc, fs);
             StringWriter sw = new StringWriter();
             TextWriter htmlTextWriter = new TextWriter(sw);


             StringReader sr = new StringReader(sw.ToString());
             HTMLWorker htmlparser = new HTMLWorker(doc);
             PdfWriter.GetInstance(doc, Response.OutputStream);
             doc.Open();
             htmlparser.Parse(sr);
             doc.Close();*/
            //string path = @"D:\VisualStudioCommunity\Projekti\NovProjekt\AplikacijaGlasovanje\MapaPrenos";
            Byte[] bytes;
            using var ms = new MemoryStream();
            iTextSharp.text.Document doc = new iTextSharp.text.Document() ;
                // using var doc = new Document();
            using var writer = PdfWriter.GetInstance(doc, ms);
            
            doc.Open();
            var html = "https://localhost:5001/Stran/RezultatiGLasovanja";

            var ww = new WebClient().DownloadString(html);
            using var htmlworker = new HTMLWorker(doc);
            using var sr = new StringReader(ww);//html
            htmlworker.Parse(sr);
            doc.Close();
            bytes = ms.ToArray();

            var tf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "rezultati.pdf");
            
            FileStream f = new FileStream(tf, FileMode.Create);            

            foreach(var b in bytes)
            {
                f.WriteByte(b);
            }

           f.Seek(0, SeekOrigin.Begin);

            for(int i = 0; i < f.Length; i++)
            {
                if(bytes[i] != f.ReadByte())
                {
                    Console.WriteLine("Napaka pri branju");
                    return null;
                }
            }

            return f;
        }
       public FileStream toPdf()
        {
            
                string path = $"D:\\VisualStudioCommunity\\Projekti\\NovProjekt\\AplikacijaExport\\{"Odgovori"} " + ".pdf";
                /*
                var pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
                ;
                PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.OpenOrCreate));
                pdfDoc.Open();

                string slikca = @"C:\Users\Marko\Pictures\Formula-1-2010-1-icon.png";
                using var fs = new FileStream(slikca, FileMode.Open);
                var png = Image.GetInstance(System.Drawing.Image.FromStream(fs), System.Drawing.Imaging.ImageFormat.Png);
                png.ScalePercent(5f);
                png.SetAbsolutePosition(pdfDoc.Left, pdfDoc.Top);
                pdfDoc.Add(png);

                var spacer = new Paragraph("");
                spacer.SpacingBefore = 10f;
                spacer.SpacingBefore = 10f;
                pdfDoc.Add(spacer);

                var headerTable = new PdfPTable(new[] { .75f, 2f });

                headerTable.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                headerTable.WidthPercentage = 75;
                headerTable.DefaultCell.MinimumHeight = 22f;

                headerTable.AddCell("Uporabnik");
                headerTable.AddCell(dzb.GetTrenutnoPrijaavljenega()[0].Email);
                headerTable.AddCell("Datum");
                headerTable.AddCell(DateTime.Now.ToString("dd/MM/yyyy"));

                pdfDoc.Add(headerTable);
                pdfDoc.Add(spacer);

                var columnCount = 5;
                var columnWidh = new[] { 2f, 2f, 2f,2f,2f };

                var table = new PdfPTable(columnWidh);

                var cel = new PdfPCell(new Phrase("Odgovori"));
                cel.Colspan = columnCount;
                cel.HorizontalAlignment = 1;
                cel.MinimumHeight = 30f;
                table.AddCell(cel);*/

                Aspose.Pdf.Document documen = new Aspose.Pdf.Document
                {
                    PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }
                };
                var pdfpage = documen.Pages.Add();
                Table t = new Table
                {
                    ColumnWidths = "20%, 20%,20%,20%,20%",
                    DefaultCellPadding = new MarginInfo(10, 5, 10, 5),
                    Border = new BorderInfo(BorderSide.All, .5f, Color.Black),
                    DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Blue)
                };
                Byte[] by;
                DataTable dt = _dzb.getDTable();
                t.ImportDataTable(dt,false,0,0);
                documen.Pages[1].Paragraphs.Add(t);
                var stream = new MemoryStream();
                documen.Save(stream);
                by = stream.ToArray();
                var f = new FileStream(path, FileMode.CreateNew);

                foreach (var b in by)
                {
                    f.WriteByte(b);
                }

                var c = new WebClient();
                c.DownloadFile("https://localhost:5001/Stran/RezultatiGLasovanja", "ss.pdf");
            return f;
        }
    }
}
