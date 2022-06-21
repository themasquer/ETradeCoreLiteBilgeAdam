using AppCore.DataAccess.Results.Bases;
using AppCore.Results;

namespace AppCore.Utils
{
    public static class FileUtil
    {
        private static Dictionary<string, string> _mimeTypes;

        static FileUtil()
        {
            _mimeTypes = new Dictionary<string, string>
            {
                { ".txt", "text/plain" },
                { ".pdf", "application/pdf" },
                { ".doc", "application/vnd.ms-word" },
                { ".docx", "application/vnd.ms-word" },
                { ".xls", "application/vnd.ms-excel" },
                { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
                { ".csv", "text/csv" },
                { ".png", "image/png" },
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".gif", "image/gif" }
            };
        }

        public static string GetContentType(string fileNameOrExtension, bool includeData = false, bool includeBase64 = false)
        {
            string contentType;
            string fileExtension = Path.GetExtension(fileNameOrExtension);
            contentType = _mimeTypes[fileExtension];
            if (includeData)
                contentType = "data:" + contentType;
            if (includeBase64)
                contentType = contentType + ";base64,";
            return contentType;
        }

        public static Result CheckFileExtension(string fileName, string acceptedFileExtensions, char acceptedFileExtensionsSeperator = ',')
        {
            Result result = new ErrorResult("Invalid file extension!");
            string fileExtension = Path.GetExtension(fileName);
            string[] acceptedFileExtensionsArray = acceptedFileExtensions.Split(acceptedFileExtensionsSeperator);
            foreach (string acceptedFileExtensionsItem in acceptedFileExtensionsArray)
            {
                if (acceptedFileExtensionsItem.Trim().Equals(fileExtension.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    result = new SuccessResult("Valid file extension.");
                    break;
                }
            }
            return result;
        }

        public static Result CheckFileLength(double fileLengthInBytes, double acceptedFileLengthInMegaBytes)
        {
            if (fileLengthInBytes > acceptedFileLengthInMegaBytes * Math.Pow(1024, 2))
                return new ErrorResult("Invalid file length!");
            return new SuccessResult("Valid file length.");
        }
    }
}
