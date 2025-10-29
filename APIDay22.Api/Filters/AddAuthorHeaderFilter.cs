using Microsoft.AspNetCore.Mvc.Filters;

namespace APIDay22.Api.Filters
{
    public class AddAuthorHeaderFilter : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Append("Author", "Mahfouz");
            base.OnResultExecuting(context);
        }
    }
}
