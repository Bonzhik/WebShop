namespace WebShop.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly string _clientId = "f2583845a2f0f81";
        private readonly string imgurAction = "https://api.imgur.com/3/image";
        private readonly HttpClient _httpClient;
        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Client-ID {_clientId}");
        }
        public async Task<string> UploadPhoto(IFormFile file)
        {
            try
            {
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(file.OpenReadStream()), "image", file.FileName);

                var response = await _httpClient.PostAsync(imgurAction, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(responseContent);
                    var imageUrl = data.data.link;
                    return imageUrl;
                }
                else
                {
                    //log
                    return $"Failed to upload image. Status code: {response.StatusCode}";
                }
            }
            catch (Exception ex)
            {
                //log
                return $"Failed -> {ex.Message}";
            }

        }
    }
}
