namespace TeisterMask.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class ExportProjectDto
    {
        [XmlElement("ProjectName")]
        public string Name { get; set; }

        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public ExportTaskDto[] Tasks { get; set; }
    }
}
//<Project TasksCount="10">
//    <ProjectName>Hyster-Yale</ProjectName>
//    <HasEndDate>No</HasEndDate>
//    <Tasks>
//      <Task>
//        <Name>Broadleaf</Name>
//        <Label>JavaAdvanced</Label>
//      </Task>
//      <Task>
//        <Name>Bryum</Name>
//        <Label>EntityFramework</Label>
//      </Task>

