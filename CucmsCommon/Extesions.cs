using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace PVAMCommon
{
    public static class Extesions
    {

        public static string Serialize<T>(this T dataToSerialize)
        {

            if (dataToSerialize == null) return null;

            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;

            MemoryStream ms = new MemoryStream();
            XmlWriter writer = XmlWriter.Create(ms, settings);

            var serializer = new XmlSerializer(dataToSerialize.GetType());
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            serializer.Serialize(writer, dataToSerialize, ns);

            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(ms);
            return sr.ReadToEnd();


        }

        public static T Deserialize<T>(this string xmlText)
        {
            if (string.IsNullOrWhiteSpace(xmlText)) return default(T);

            using (StringReader stringReader = new StringReader(xmlText))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T) serializer.Deserialize(stringReader);
            }
        }

        public static List<string> ParseToList(this string input, char delimiter)
        {
            return input.Split(delimiter).ToList();
        }


        public static string ToCamelCase(this string xml, string xsl)
        {
            using (var srt = new StringReader(xsl)) // xslInput is a string that contains xsl
            using (var sri = new StringReader(xml)) // xmlInput is a string that contains xml
            {
                using (var xrt = XmlReader.Create(srt))
                using (var xri = XmlReader.Create(sri))
                {
                    var xslt = new XslCompiledTransform();
                    xslt.Load(xrt);
                    using (var sw = new StringWriter())
                    using (var xwo = XmlWriter.Create(sw, xslt.OutputSettings)) // use OutputSettings of xsl, so it can be output as HTML
                    {
                        xslt.Transform(xri, xwo);
                        return xwo.ToString();
                    }
                    
                }
               
            }
        }


        public static IQueryable<object> Set(this DbContext _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }


        public static IQueryable<object> Set(this DbContext _context, string table)
        {
            Type TableType = _context.GetType().Assembly.GetExportedTypes().FirstOrDefault(t => t.Name == table);
            IQueryable<object> ObjectContext = _context.Set(TableType);
            return ObjectContext;
        }
    }
}
