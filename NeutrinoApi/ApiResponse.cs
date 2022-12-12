using System;
using System.Text.Json;

namespace NeutrinoApi
{
    /// <summary>
    ///  API response payload, holds the response data along with any error details
    /// </summary>
    public class ApiResponse
    {
        private const int NoStatus = 0;
        private const string NoContentType = "";
        private const int NoErrorCode = 0;
        private const string NoErrorMsg = "";

        private ApiResponse(JsonElement data, string file, string contentType, int httpStatusCode, int errorCode,
            string errorMessage, Exception errorCause)
        {
            this.Data = data;
            this.File = file;
            this.ContentType = contentType;
            this.HttpStatusCode = httpStatusCode;
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
            this.ErrorCause = errorCause;
        }

        /// <summary>
        /// The response data for JSON based APIs
        /// </summary>
        public JsonElement Data { get; }

        /// <summary>
        /// The local file path storing the output for file based APIs
        /// </summary>
        public string File { get; }

        /// <summary>
        /// The response content type (MIME type)
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// The HTTP status code returned
        /// </summary>
        public int HttpStatusCode { get; }

        /// <summary>
        /// The API error code if any error has occurred
        /// </summary>
        public int ErrorCode { get; }

        /// <summary>
        /// The API error message if any error has occurred
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// For client-side errors or exceptions get the underlying cause
        /// </summary>
        public Exception ErrorCause { get; }

        /// <summary>
        /// Was this request successul
        /// </summary>
        /// <returns></returns>
        public bool IsOK()
        {
            return Data.ValueKind == JsonValueKind.Object || File != null;
        }

        /// <summary>
        /// Create an API response for JSON data
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="contentType"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ApiResponse OfData(int statusCode, string contentType, JsonElement data)
        {
            return new ApiResponse(data, default, contentType, statusCode, NoErrorCode, NoErrorMsg, default);
        }

        /// <summary>
        /// Create an API response for file data
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="contentType"></param>
        /// <param name="outputFilePath"></param>
        /// <returns></returns>
        public static ApiResponse OfOutputFilePath(int statusCode, string contentType, string outputFilePath)
        {
            return new ApiResponse(default, outputFilePath, contentType, statusCode, NoErrorCode, NoErrorMsg, default);
        }

        /// <summary>
        /// Create an API response for error code
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="contentType"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static ApiResponse OfErrorCode(int statusCode, string contentType, int errorCode)
        {
            var errorMessage = ApiErrorCode.GetErrorMessage(errorCode);
            return new ApiResponse(default, default, contentType, statusCode, errorCode, errorMessage, default);
        }

        /// <summary>
        /// Create an API response for status code
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="contentType"></param>
        /// <param name="errorCode"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static ApiResponse OfHttpStatus(int statusCode, string contentType, int errorCode, string errorMessage)
        {
            return new ApiResponse(default, default, contentType, statusCode, errorCode, errorMessage, default);
        }

        /// <summary>
        /// Create an API response for error cause
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="errorCause"></param>
        /// <returns></returns>
        public static ApiResponse OfErrorCause(int errorCode, Exception errorCause)
        {
            var errorMessage = ApiErrorCode.GetErrorMessage(errorCode);
            return new ApiResponse(default, default, NoContentType, NoStatus, errorCode, errorMessage, errorCause);
        }
    }
}