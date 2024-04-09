
namespace NTI.Application.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetError(this Exception e) => e.InnerException?.Data["Detail"]?.ToString() ?? e?.InnerException?.Message ?? e.Message;
    }
}