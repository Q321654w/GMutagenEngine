namespace GmutagenEngine.Requests
{
    public abstract class BaseResponse : IResponse
    {
        public int StatusCode { get; set; }
        public List<ErrorEntry> Errors { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public string RawBody { get; set; }

        public virtual void Dispose() { }
    }
}