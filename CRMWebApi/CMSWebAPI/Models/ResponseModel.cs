using CMSWebAPI.DbModels.Enums;

namespace CMSWebAPI.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; }
        public ResponseStatus Status { get; set; }
        public string ResponseMessage { get; set; }
        public object ResponseData { get; set; }       
    }
}
