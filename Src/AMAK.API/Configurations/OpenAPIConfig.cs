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
        }
    }
}