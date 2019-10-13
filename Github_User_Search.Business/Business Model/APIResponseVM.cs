using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Business.Business_Model
{
    public class ResponseVM<T>
    {
        public ResponseVM(T data)
        {
            Data = data;
        }
        public ResponseVM(bool error, string message)
        {
            Error = error;
            Message = message;
        }

        public bool Error { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
