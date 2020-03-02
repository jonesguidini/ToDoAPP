namespace APP.Domain.EntitiesConfig
{
    public class Notification
    {
        public Notification(string _key, string _message)
        {
            Key = _key;
            Message = _message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
