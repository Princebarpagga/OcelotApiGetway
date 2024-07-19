namespace WebApi.Domain.Model
{
    public class ServiceResponse
    {
        public object data { get; set; }
        public object dataInfo { get; set; }

        public int statuscode { get; set; }

        public bool isSuccess { get; set; }

    }
}
