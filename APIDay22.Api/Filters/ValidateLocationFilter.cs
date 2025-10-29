

using APIDay22.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APIDay22.Core.Filters
{
     public class ValidateLocationFilter : ActionFilterAttribute
    {
        private static readonly HashSet<string> AllowedLocations = new(StringComparer.OrdinalIgnoreCase)
        {
            "USA", "EG"
        };

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dept = context.ActionArguments.Values.OfType<Department>().FirstOrDefault();
            if (dept == null)
            {
                base.OnActionExecuting(context);
                return;
            }

            if (!AllowedLocations.Contains(dept.Location?.Trim() ?? string.Empty))
            {
                var controller = (Controller)context.Controller;
                controller.ModelState.AddModelError(nameof(Department.Location), "Location must be USA or EG");
                context.Result = new BadRequestObjectResult(controller.ModelState);
            }

            base.OnActionExecuting(context);
        }
    }
}
