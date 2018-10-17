using CucmsService.Interfaces;
using PracticeVelocityDomain.DTOs;
using PVAMCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CucmsService.Services
{
    public class SourceService : ISourceService
    {
        public async Task<IList<Log_Record>> GetPatientRecordsAsync(string filename)
        {
            var sourceXml = await GetXmlFromFileAsync(filename);
            if (sourceXml != null)
            {
                var record = sourceXml.Deserialize<NewRecord>();
                return record.Log_Record.Any() ? record.Log_Record : new List<Log_Record>();
            }
            return new List<Log_Record>();
        }

        public async Task<IList<string>> GetSourceFilesAsync(string filePath, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                await Task.Run(() => {
                    var dir = filePath;
                    var allFiles = Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories)
                        .Where(s => s.EndsWith("xml") && s.Contains("new")).ToList();
                    var appPath = AppDomain.CurrentDomain.BaseDirectory + @"ProcessFileLog.txt";
                    return allFiles;
                });               
            }
            if (File.Exists(filePath + fileName))
            {
                return new List<string> { filePath + fileName };
            }
            return new List<string>();
        }

        public async Task<string> GetXmlFromFileAsync(string  filepath)
        {
            var resturnString = string.Empty;
            if (File.Exists(filepath))
            {
                var xmlDoc = new XmlDocument();
              
                    await Task.Run(() =>
                    {
                        xmlDoc.Load(filepath);
                        using (var stringWriter = new StringWriter())
                        {
                            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                            {
                                xmlDoc.WriteTo(xmlTextWriter);
                                xmlTextWriter.Flush();
                                resturnString = stringWriter.GetStringBuilder().ToString();
                            };
                        }
                    });                       
            }
            return resturnString;
        }
    }
}
