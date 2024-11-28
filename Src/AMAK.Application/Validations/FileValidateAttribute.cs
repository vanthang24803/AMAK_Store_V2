using System.Net;
using AMAK.Application.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AMAK.Application.Validations {
    public class FileValidateAttribute : ActionFilterAttribute {
        private readonly string[] _allowedImageExtensions = [".jpg", ".svg", ".png", ".webp"];
        private readonly string[] _allowedOtherExtensions = [".xlsx", ".csv"];
        private readonly long _maxFileSize = 5 * 1024 * 1024;

        public override void OnActionExecuting(ActionExecutingContext context) {
            var files = context.HttpContext.Request.Form.Files;

            foreach (var file in files) {
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (_allowedImageExtensions.Contains(extension)) {
                    if (file.Length > _maxFileSize) {
                        context.Result = new BadRequestObjectResult(new ApiError() {
                            Status = (int)HttpStatusCode.BadRequest,
                            Message = "Max file size for images is 5MB.",
                            Timestamp = DateTime.Now
                        });
                        return;
                    }
                } else if (!_allowedOtherExtensions.Contains(extension)) {
                    context.Result = new BadRequestObjectResult(
                        new ApiError() {
                            Status = (int)HttpStatusCode.BadRequest,
                            Message = $"File extension {extension} is not allowed.",
                            Timestamp = DateTime.Now
                        }
                        );
                    return;
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
