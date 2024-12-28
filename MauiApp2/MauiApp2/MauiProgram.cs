using Microsoft.Extensions.Logging;

namespace MauiApp2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            var baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5289/" : "http://localhost:5289/";
            builder.Services.AddScoped(x => new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            });
            return builder.Build();
        }
    }
}
