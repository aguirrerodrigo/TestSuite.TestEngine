namespace TestSuite.TestManagement.Web.Extensions
{
    public static class ExecutionStatusExtensions
    {
        public static string ToCss(this ExecutionStatus status)
        {
            switch (status)
            {
                case ExecutionStatus.New:
                    return $"muted";
                case ExecutionStatus.InProgress:
                    return $"info";
                case ExecutionStatus.Passed:
                    return $"success";
                case ExecutionStatus.Failed:
                    return $"danger";
                default:
                    return string.Empty;
            }
        }

        public static string ToCssIcon(this ExecutionStatus status)
        {
            switch (status)
            {
                case ExecutionStatus.New:
                    return $"question";
                case ExecutionStatus.InProgress:
                    return $"play";
                case ExecutionStatus.Passed:
                    return $"check";
                case ExecutionStatus.Failed:
                    return $"times";
                default:
                    return string.Empty;
            }
        }
    }
}