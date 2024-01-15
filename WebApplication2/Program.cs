using DNTCaptcha.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDNTCaptcha(options =>
{
    // options.UseSessionStorageProvider() // -> It doesn't rely on the server or client's times. Also it's the safest one.
    // options.UseMemoryCacheStorageProvider() // -> It relies on the server's times. It's safer than the CookieStorageProvider.
    options
        .UseCookieStorageProvider( /* If you are using CORS, set it to `None` */) // -> It relies on the server and client's times. It's ideal for scalability, because it doesn't save anything in the server's memory.
                                                                                  // .UseDistributedCacheStorageProvider() // --> It's ideal for scalability using `services.AddStackExchangeRedisCache()` for instance.
                                                                                  // .UseDistributedSerializationProvider()

        // Don't set this line (remove it) to use the installed system's fonts (FontName = "Tahoma").
        // Or if you want to use a custom font, make sure that font is present in the wwwroot/fonts folder and also use a good and complete font!
        .UseCustomFont(Path.Combine(builder.Environment.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf"))
        .ShowExceptionsInResponse(builder.Environment.IsDevelopment())
        .AbsoluteExpiration(7)
        .RateLimiterPermitLimit(10) // for .NET 7x, Also you need to call app.UseRateLimiter() after calling app.UseRouting().
        .ShowThousandsSeparators(false)
        .WithNoise(0.015f, 0.015f, 1, 0.0f)
        .WithEncryptionKey("This is my secure key!")
        .WithNonceKey("NETESCAPADES_NONCE")
        .InputNames( // This is optional. Change it if you don't like the default names.
                    new DNTCaptchaComponent
                    {
                        CaptchaHiddenInputName = "DNT_CaptchaText",
                        CaptchaHiddenTokenName = "DNT_CaptchaToken",
                        CaptchaInputName = "DNT_CaptchaInputText",
                    })
        .Identifier("dnt_Captcha") // This is optional. Change it if you don't like its default name.
        ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
