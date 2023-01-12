using LibraryApp.Entities.Enums;
using System.Collections.Generic;

namespace LibraryApp.Entities.Models
{
    public class ServiceResult
    {
        public ServiceResult()
        {
            Messages = new List<string>();
            ResultType = ResultType.Success;
            Data = null;
        }

        public ResultType ResultType { get; set; }

        public List<string> Messages { get; set; }

        public object Data { get; set; }
    }

}
