using Scalar.AspNetCore;

namespace AMAK.API.Configurations {
    public static class OpenAPIConfig {
        public static void UseAPIDocs(this WebApplication app) {
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapGet("/", (HttpContext httpContext) => {
                httpContext.Response.Redirect("/swagger", permanent: false);
                return Results.Empty;
            });


            // app.UseSwagger(opt => {
            //     opt.RouteTemplate = "openapi/{documentName}.json";

            // });
            // app.MapScalarApiReference(opt => {
            //     opt.Title = "AMAK API Docs";
            //     opt.Theme = ScalarTheme.DeepSpace;
            //     opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
            // });

            // app.MapGet("/", (HttpContext httpContext) => {
            //     httpContext.Response.Redirect("/scalar", permanent: false);
            //     return Results.Empty;
            // });


        }
    }
}