namespace TeisterMask.DataProcessor.ExportDto
{
    using System.Xml.Serialization;

    [XmlType("Task")]
    public class ExportTaskDto
    {
        [XmlElement("Name")]
        public string TaskName { get; set; }

        [XmlElement("Label")]
        public string Label { get; set; }
    }
}

//< Tasks >
//      <Task>
//        <Name>Broadleaf</Name>
//        <Label>JavaAdvanced</Label>
//      </Task>
