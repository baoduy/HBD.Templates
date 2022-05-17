using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FluentAssertions;
using Xunit;

namespace TemplateTests
{
    public class UnitTest
    {
        [Fact]
        public void Missing_Files()
        {
            const string path = "../../../../../";
            var templates = Directory.GetFiles(path, "*.vstemplate", SearchOption.AllDirectories);

            var serializer = new XmlSerializer(typeof(VSTemplate), "");
            var settings = new XmlReaderSettings();

            var notfoundFiles = new List<string>();
            var fileNotIn = new List<string>();
            var notfoundProject = new List<string>();

            foreach (var t in templates)
            {
                try
                {
                    using var textReader = File.OpenRead(t);
                    using var xmlReader = XmlReader.Create(textReader, settings);
                    var data = (VSTemplate) serializer.Deserialize(xmlReader);

                    if (data?.TemplateContent.Project != null)
                    {
                        notfoundFiles.AddRange(VerifyProject(data.TemplateContent.Project, Path.GetDirectoryName(t)));
                        fileNotIn.AddRange(VerifyFileNotInProject(data.TemplateContent.Project, Path.GetDirectoryName(t)));
                    }
                    else if (data?.TemplateContent.ProjectCollection != null)
                    {
                        notfoundProject.AddRange(VerifySolution(data.TemplateContent.ProjectCollection, Path.GetDirectoryName(t)));
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Cannot read file: {t}", ex);
                }
            }

            notfoundProject.Select(n => n.Replace(path, string.Empty)).Should().BeEmpty("Projects Not Found.");
            notfoundFiles.Select(n => n.Replace(path, string.Empty)).Should().BeEmpty("Files Not Found.");
            fileNotIn.Select(n => n.Replace(path, string.Empty)).Should().BeEmpty("This Files are not in the Project Templates.");
        }

        private static IEnumerable<string> VerifySolution(ProjectCollection project, string rootFolder)
            => project.SolutionFolder
                .SelectMany(s => s.ProjectTemplateLink, (s, i) => Path.Combine(rootFolder, i.Text.Replace("\n", string.Empty).Replace("\\", "/").Trim()))
                .Where(file => !File.Exists(file))
                .ToList();

        private static IEnumerable<string> VerifyProject(Project data, string rootFolder)
        {
            var notFound = new List<string>();

            void VerifyItem(ProjectItem item, string folder)
            {
                var file = Path.Combine(folder, item.Text);
                if (!File.Exists(file))
                    notFound.Add(file);
            }

            foreach (var f in data.Folder)
            foreach (var i in f.ProjectItem)
                VerifyItem(i, Path.Combine(rootFolder, f.Name));

            foreach (var i in data.ProjectItem)
                VerifyItem(i, rootFolder);

            return notFound;
        }

        private static IEnumerable<string> VerifyFileNotInProject(Project data, string rootFolder)
        {
            var notFound = new List<string>();

            void VerifyItem(IEnumerable<ProjectItem> items, string folder)
            {
                notFound.AddRange(Directory.GetFiles(folder, "*.cs", SearchOption.TopDirectoryOnly).Where(f => !items.Any(i => f.EndsWith(i.Text))));
            }

            VerifyItem(data.ProjectItem, rootFolder);

            foreach (var f in data.Folder)
                VerifyItem(f.ProjectItem, Path.Combine(rootFolder, f.Name));
            return notFound;
        }
    }
}