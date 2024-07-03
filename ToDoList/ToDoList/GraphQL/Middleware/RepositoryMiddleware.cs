namespace ToDoList.GraphQL.Middleware
{
    public class RepositoryMiddleware
    {
        private readonly RequestDelegate _next;
        public RepositoryMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var repositoryTypeString = context.Request.Headers["Storage"].FirstOrDefault();

            if (!string.IsNullOrEmpty(repositoryTypeString))
                context.Items["StorageType"] = repositoryTypeString;

            await _next(context);
        }
    }
}
