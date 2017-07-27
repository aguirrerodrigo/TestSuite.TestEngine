using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TestSuite.TestManagement
{
    public class TestCaseExecution
    {
        public static TestCaseExecution FromXml(string xml)
        {
            var result = default(TestCaseExecution);
            try
            {
                var xmlSerializer = new XmlSerializer(typeof(TestCaseExecution));
                using (var reader = new StringReader(xml))
                {
                    result = xmlSerializer.Deserialize(reader) as TestCaseExecution;
                }
            }
            catch (Exception ex)
            {
                result = new TestCaseExecution();
                result.Error = ex.Message;
            }

            return result;
        }

        [XmlIgnore]
        public string Name { get; set; }
        [XmlIgnore]
        public DateTime CreatedDateTime { get; set; }
        [XmlIgnore]
        public string Error { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }
        public ExecutionStatus Status { get; set; }
        public TestStepCollection Steps { get; set; } = new TestStepCollection();

        public TestCaseExecution() { }

        public string ToXml()
        {
            var result = default(string);

            var xmlSerializer = new XmlSerializer(typeof(TestCaseExecution));
            using (var stringWriter = new StringWriter())
            using (var writer = XmlWriter.Create(stringWriter))
            {
                xmlSerializer.Serialize(writer, this);
                result = stringWriter.ToString();
            }

            return result;
        }
    }
}
