using GraphQL.Types;
using CategoryAPI.Mutations;
using CategoryAPI.Queries;

namespace CategoryAPI.Schemas
{
    public class CategorySchema:Schema
    {
        public CategorySchema(IServiceProvider ServiceProvider)
        {
            Query = ServiceProvider.GetRequiredService<CategoryGLQuery>();
            Mutation = ServiceProvider.GetRequiredService<CategoryMutation>();
        }
    }
}
