namespace dotnet_meter_api.Models.Responses
{
    public class ErroredJsonResponse
    {
        public string ErroredJson { get; set; }

        public ErroredJsonResponse(string body)
        {
            this.ErroredJson = body;
        }
    }
}