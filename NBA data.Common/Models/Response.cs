namespace NBA_data.Common.Models
{
    using System;
    public class Response
    {
        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public string Message { get; set; }
    }
}
