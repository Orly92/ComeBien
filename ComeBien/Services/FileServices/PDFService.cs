using ComeBien.DataAccess.Repositories;
using ComeBien.Models.Core;
using ComeBien.Services.FileServices;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComeBien.Services
{
    public class PDFService : IFileService
    {
        private readonly IOrderRepository _orderRepository;
        public PDFService()
        {
            _orderRepository = new OrderRepository();
        }

        public async Task ExportOrders(DateTime dateInitial, DateTime dateEnd)
        {
            Log.Information($"Salvando documento pdf para las ordenes de {dateInitial} a {dateEnd}");
            IList<OrderDTO> orders = await _orderRepository.GetOrders(dateInitial, dateEnd);

            Log.Information("Ordenes extraidas");

            PdfDocument document = new PdfDocument();
            document.Info.Title = ComeBien.Resources.Resources.ResourceManager.GetString("OrderPDFTitle");

            List<PdfPage> pdfPageList = new List<PdfPage> { };
            List<XGraphics> graphList = new List<XGraphics> { };
            List<XTextFormatter> tfList = new List<XTextFormatter> { };
            for (int j = 0; j < orders.Count; j += 33)
            {
                PdfPage pdfPage = document.AddPage();
                pdfPage.Height = 700;
                pdfPage.Width = 500;
                XGraphics graph = XGraphics.FromPdfPage(pdfPage);

                XTextFormatter tf = new XTextFormatter(graph);

                pdfPageList.Add(pdfPage);
                graphList.Add(graph);
                tfList.Add(tf);
            }

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            XStringFormat format = new XStringFormat();
            format.LineAlignment = XLineAlignment.Near;
            format.Alignment = XStringAlignment.Near;


            XFont fontParagraph = new XFont("Arial", 8, XFontStyle.Regular);

            int el_width = 100;

            double lineHeight = 20;
            int marginLeft = 20;
            int marginTop = 20;

            int el_height = 30;
            int rect_height = 17;

            int interLine = 2;

            int offSetX = el_width;

            XSolidBrush rect_style = new XSolidBrush(XColors.LightBlue);

            graphList[0].DrawRectangle(rect_style, marginLeft, marginTop, pdfPageList[0].Width - 2 * marginLeft, rect_height);

            IList<string> headers = new List<string>();
            headers.Add("Id");
            headers.Add(ComeBien.Resources.Resources.ResourceManager.GetString("BuyinDate"));
            headers.Add($"{ComeBien.Resources.Resources.ResourceManager.GetString("Product")}s");
            headers.Add(ComeBien.Resources.Resources.ResourceManager.GetString("Total"));
            
            for (int i = 0; i < headers.Count; i++)
            {

                tfList[0].DrawString(headers.ElementAt(i), fontParagraph, XBrushes.White,
                              new XRect(marginLeft + i * (offSetX + interLine), marginTop, el_width, el_height), format);


            }
            int heightindex = 0;
            int pageIndex = 0;
            for (int j = 0; j < orders.Count; j++)
            {
                if (j > 0 && j % 33 == 0)
                {
                    heightindex = 0;
                    pageIndex++;
                }
                double dist_Y = lineHeight * (heightindex + 1);
                int i = 0;

                var order = orders.ElementAt(j);
                
                tfList[pageIndex].DrawString(
                    order.Id.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(marginLeft + i * (offSetX + interLine), marginTop + dist_Y, el_width, el_height),
                    format);
                i++;

                tfList[pageIndex].DrawString(
                    order.TimeStamp.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(marginLeft + i * (offSetX + interLine), marginTop + dist_Y, el_width, el_height),
                    format);
                i++;

                tfList[pageIndex].DrawString(
                    order.OrderProducts.Count.ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(marginLeft + i * (offSetX + interLine), marginTop + dist_Y, el_width, el_height),
                    format);
                i++;

                tfList[pageIndex].DrawString(
                    Math.Round(order.TotalAmount,2).ToString(),
                    fontParagraph,
                    XBrushes.Black,
                    new XRect(marginLeft + i * (offSetX + interLine), marginTop + dist_Y, el_width, el_height),
                    format);

                heightindex++;
            }

            const string filename = "Resources/Files/orders.pdf";
            document.Save(filename);

            Log.Information("Documento pdf salvado");
        }
    }
}
