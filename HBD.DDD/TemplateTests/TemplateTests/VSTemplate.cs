using System.Collections.Generic;
using System.Xml.Serialization;

namespace TemplateTests
{
    [XmlRoot(ElementName = "ProjectItem", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class ProjectItem
    {
        [XmlAttribute(AttributeName = "ReplaceParameters")]
        public string ReplaceParameters { get; set; }

        [XmlAttribute(AttributeName = "TargetFileName")]
        public string TargetFileName { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Folder", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class Folder
    {
        [XmlElement(ElementName = "ProjectItem", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public List<ProjectItem> ProjectItem { get; set; } = new();

        [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }

        [XmlAttribute(AttributeName = "TargetFolderName")]
        public string TargetFolderName { get; set; }
    }

    [XmlRoot(ElementName = "Project", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class Project
    {
        [XmlElement(ElementName = "Folder", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public List<Folder> Folder { get; set; } = new();

        [XmlElement(ElementName = "ProjectItem", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public List<ProjectItem> ProjectItem { get; set; } = new();

        // [XmlAttribute(AttributeName = "TargetFileName")]
        // public string TargetFileName { get; set; }
        //
        // [XmlAttribute(AttributeName = "File")] public string File { get; set; }
        //
        // [XmlAttribute(AttributeName = "ReplaceParameters")]
        // public string ReplaceParameters { get; set; }
    }

    [XmlRoot(ElementName = "TemplateContent", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class TemplateContent
    {
        [XmlElement(ElementName = "Project", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public Project Project { get; set; }

        [XmlElement(ElementName = "ProjectCollection", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public ProjectCollection ProjectCollection { get; set; }
    }

    [XmlRoot(ElementName = "VSTemplate", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class VSTemplate
    {
        [XmlElement(ElementName = "TemplateContent", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public TemplateContent TemplateContent { get; set; } = new();
    }

    [XmlRoot(ElementName = "ProjectTemplateLink", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class ProjectTemplateLink
    {
        [XmlAttribute(AttributeName = "ProjectName")]
        public string ProjectName { get; set; }

        [XmlAttribute(AttributeName = "CopyParameters")]
        public string CopyParameters { get; set; }

        [XmlText] public string Text { get; set; }
    }

    [XmlRoot(ElementName = "SolutionFolder", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class SolutionFolder
    {
        [XmlElement(ElementName = "ProjectTemplateLink", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public List<ProjectTemplateLink> ProjectTemplateLink { get; set; } = new();

        [XmlAttribute(AttributeName = "Name")] public string Name { get; set; }
    }

    [XmlRoot(ElementName = "ProjectCollection", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
    public class ProjectCollection
    {
        [XmlElement(ElementName = "SolutionFolder", Namespace = "http://schemas.microsoft.com/developer/vstemplate/2005")]
        public List<SolutionFolder> SolutionFolder { get; set; } = new();
    }
}