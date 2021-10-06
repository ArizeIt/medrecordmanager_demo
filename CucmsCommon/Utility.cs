using iText.IO.Image;
using iText.IO.Source;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace PVAMCommon
{
    public static class Utility
    {

        static ILoggerFactory _loggerFactory;

        public static void ConfigureLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }


        public static ILogger CreateLogger<T>()
        {
            if (_loggerFactory == null)
            {
                throw new InvalidOperationException($"{nameof(ILogger)} is not configured. {nameof(ConfigureLogger)} must be called before use");
                //_loggerFactory = new LoggerFactory().AddConsole().AddDebug();
            }

            return _loggerFactory.CreateLogger<T>();
        }
        public static byte[] CreatePDF(byte[] source)
        {
            var baos = new ByteArrayOutputStream();
            var pdfDoc = new PdfDocument(new PdfWriter(baos));
            var document = new Document(pdfDoc);
            var pageCount = TiffImageData.GetNumberOfPages(source);

            for (int i = 1; i <= pageCount; i++)
            {
                var tiffImage = ImageDataFactory.CreateTiff(source, true, i, true);
                var tiffPageSize = new iText.Kernel.Geom.Rectangle(tiffImage.GetWidth(), tiffImage.GetHeight());
                var newPage = pdfDoc.AddNewPage(new PageSize(tiffPageSize));
                var canvas = new PdfCanvas(newPage);
                canvas.AddImage(tiffImage, tiffPageSize, false);
            }

            document.Close();
            return baos.ToArray();
        }
        public static string FormatAmdName(string fName, string lName, string mName)
        {
            string returnName;
            if (string.IsNullOrEmpty(mName))
            {
                returnName = lName + "," + fName;
            }
            else
            {
                returnName = lName + "," + fName + " " + mName;
            }
            return returnName;
        }

        public static void ParseCptCode(string originalcode, out string code, out int quantity, out string modifier1, out string modifier2)
        {
            code = string.Empty;
            quantity = 1;
            modifier1 = string.Empty;
            modifier2 = string.Empty;

            if (!string.IsNullOrEmpty(originalcode))
            {
                if (!originalcode.Contains(','))
                {
                    code = originalcode;
                }
                else
                {
                    var result = originalcode.Split(',');

                    if (result.Count() == 2)
                    {
                        if (result[0].Length == 5)
                        {
                            modifier1 = result[1];
                            code = result[0];
                        }
                        if (result[1].Length == 5)
                        {
                            quantity = short.Parse(result[0]);
                            code = result[1];
                        }
                    }

                    if (result.Count() == 3)
                    {
                        if (result[0].Length != 5 && short.TryParse(result[0], out short codeQuantity))
                        {
                            quantity = codeQuantity;
                            code = result[1];
                            modifier1 = result[2];
                        }
                        else if (!short.TryParse(result[0], out codeQuantity))
                        {
                            code = result[0];
                            modifier1 = result[1];
                            modifier2 = result[2];
                            codeQuantity = 1;
                        }
                    }
                    if (result.Count() == 4)
                    {
                        quantity = short.Parse(result[0]);
                        code = result[1];
                        modifier1 = result[2];
                        modifier2 = result[3];
                    }
                }
            }
        }

        public static void ParseModifierCode(string modifiercodes, out string modifier1, out string modifier2)
        {
            modifier1 = string.Empty;

            modifier2 = string.Empty;

            if (!string.IsNullOrEmpty(modifiercodes))
            {
                if (!modifiercodes.Contains(','))
                {
                    modifier1 = modifiercodes;
                }
                else
                {
                    var result = modifiercodes.Split(',');
                    modifier1 = result[0];
                    modifier2 = result[1];
                }
            }
        }
        public static void Method(out int answer, out string message, out string stillNull)
        {
            answer = 44;
            message = "I've been returned";
            stillNull = null;
        }
    }
}
