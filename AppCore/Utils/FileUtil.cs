using AppCore.DataAccess.Results.Bases;
using AppCore.Results;

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
            if (contentType != "")
            {
                if (includeData)
                    contentType = "data:" + contentType;
                if (includeBase64)
                    contentType = contentType + ";base64,";
            }
            return contentType;
        }

        public static Result CheckFileExtension(string fileExtension, string acceptedFileExtensions, char acceptedFileExtensionsSeperator = ',')
        {
            Result result = new ErrorResult("Invalid file extension!");
            string[] acceptedFileExtensionsArray = acceptedFileExtensions.Split(acceptedFileExtensionsSeperator);
            foreach (string acceptedFileExtensionsItem in acceptedFileExtensionsArray)
            {
                if (acceptedFileExtensionsItem.ToLower().Trim() == fileExtension.ToLower().Trim())
                {
                    result = new SuccessResult("Valid file extension.");
                    break;
                }
            }
            return result;
        }

        public static Result CheckFileLength(double fileLengthInBytes, double acceptedFileLengthInMegaBytes = 1)
        {
            if (fileLengthInBytes > acceptedFileLengthInMegaBytes * Math.Pow(1024, 2))
                return new ErrorResult("Invalid file length!");
            return new SuccessResult("Valid file length.");
        }
    }
}
