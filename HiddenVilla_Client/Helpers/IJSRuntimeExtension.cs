using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace HiddenVilla_Client.Helpers
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "success", message);
        }

        public static async ValueTask ToastrFailure(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("showToastr", "error", message);
        }

        public static async ValueTask SwalSuccess(this IJSRuntime jsRuntime, string message, string title)
        {
            await jsRuntime.InvokeVoidAsync("showSwal", "success", message, title);
        }

        public static async ValueTask SwalFailure(this IJSRuntime jsRuntime, string message, string title)
        {
            await jsRuntime.InvokeVoidAsync("showSwal", "error", message, title);
        }
    }
}