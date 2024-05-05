namespace WebShop.Helpers
{
    public static class MimeMapper
    {
        public static readonly IDictionary<string, string> mappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            {".jpg", "image/jpeg" },
            {".png", "image/png" },
            {".bmp", "image/bmp"},
            {".gif", "image/gif"}
        };
    }
}
