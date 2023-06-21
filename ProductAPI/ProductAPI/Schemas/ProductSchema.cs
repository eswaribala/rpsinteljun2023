using GraphQL.Types;
using ProductAPI.Mutations;
using ProductAPI.Queries;

namespace InventoryAPI.Schemas
{
    public class ProductSchema:Schema
    {
        public ProductSchema(IServiceProvider ServiceProvider)
        {
            Query = ServiceProvider.GetRequiredService<ProductGLQuery>();
            Mutation = ServiceProvider.GetRequiredService<ProductMutation>();
        }
    }
}
