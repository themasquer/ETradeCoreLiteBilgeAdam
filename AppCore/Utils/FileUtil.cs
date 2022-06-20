namespace AppCore.Utils
{
    public static class FileUtil
    {
        public static string GetContentType(string fileExtension, bool includeData = false, bool includeBase64 = false)
        {
            string contentType = "";
            if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                contentType = "image/jpeg";
            else if (fileExtension == ".png")
                contentType = "image/png";
            if (includeData)
                contentType = "data:" + contentType;
            if (includeBase64)
                contentType = contentType + ";base64,";
            return contentType;
        }
    }
}
