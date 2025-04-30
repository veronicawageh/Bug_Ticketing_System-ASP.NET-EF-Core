using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BugTicketingSystem
{
    public class GeneralResult
    {
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ResultError[]? Errors { get; set; } = null;

    }

    public class ResultError
    {

        public string Message { get; set; } = string.Empty;
    }

    // Generic class that inherits from GeneralResult
    public class GeneralResult<T> : GeneralResult

    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; } // Add the Data property of type T
    }
}
