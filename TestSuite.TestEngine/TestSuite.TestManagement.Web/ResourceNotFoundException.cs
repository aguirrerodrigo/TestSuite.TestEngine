using System;
using System.Web;

namespace TestSuite.TestManagement.Web.ViewModels
{
    public class ResourceNotFoundException : HttpException
    {
        public ResourceNotFoundException(string message) : base(404, message)
        {
        }

        public ResourceNotFoundException(string message, Exception innerException) : base(404, message, innerException)
        {
        }
    }
}