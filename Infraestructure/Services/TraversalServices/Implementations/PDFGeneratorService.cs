using Core.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using TraversalServices.Interfaces;

namespace TraversalServices.Implementations
{
    public class PDFGeneratorService : IPDFGeneratorService        
    {

        #region constructor

        public PDFGeneratorService() { }

        #endregion


        #region public methods

        public async Task<byte[]> ObjectToPDFAsync<U>(U obj) where U : class
        {
            var doc = new Document(PageSize.A4);
            var stream = new MemoryStream();
            PdfWriter.GetInstance(doc, stream);
            doc.Open();
            doc.Add(GetHeaderTable());
            doc.Add(GetPDFDataTable(obj));
            doc.Close();
            return stream.ToArray();
        }

        #endregion

        #region private methods

        private PdfPTable GetPDFDataTable<U>(U obj)
        {
            var type = obj.GetType();
            var font = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.NORMAL);
            var bold = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);
            var table = new PdfPTable(2);


            table.WidthPercentage = 100f;
            table.DefaultCell.Border = 0;


            foreach(PropertyInfo prop in type.GetProperties())
            {

                if ( prop.PropertyType == typeof(DateTime))
                {
                    table.AddCell(new PdfPCell(new Paragraph(prop.Name.ToUpper(), bold)));
                    table.AddCell(new PdfPCell(new Paragraph(((DateTime) prop.GetValue(obj)).ToString("u", CultureInfo.CreateSpecificCulture("es-ES")), font)));
                }
                else
                {
                    table.AddCell(new PdfPCell(new Paragraph(prop.Name.ToUpper(), bold)));
                    table.AddCell(new PdfPCell(new Paragraph(prop.GetValue(obj).ToString(), font)));
                }
                
            }

            return table;

        }

        private PdfPTable GetHeaderTable()
        {
            var font = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.NORMAL);
            var bold = new Font(Font.FontFamily.TIMES_ROMAN, 12, Font.BOLD);
            var table = new PdfPTable(1);
            var image = new StreamReader($"{Directory.GetCurrentDirectory()}/../Infraestructure/Services/TraversalServices/Resources/logo.png");
            var logo = Image.GetInstance(image.BaseStream);
            logo.ScalePercent(15);

            table.WidthPercentage = 100f;            
            
            PdfPCell logo_cell = new PdfPCell(logo);
            logo_cell.Border = 0;
            table.AddCell(logo_cell);

            return table;

        }

        #endregion
    }
}
