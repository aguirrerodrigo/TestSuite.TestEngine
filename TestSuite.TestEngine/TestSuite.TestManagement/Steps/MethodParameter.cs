namespace TestSuite.TestManagement
{
    public class MethodParameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public MethodParameter() { }

        public MethodParameter(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string GetFormattedName()
        {
            return this.Name?.Trim().Replace(" ", "_");
        }
    }
}
