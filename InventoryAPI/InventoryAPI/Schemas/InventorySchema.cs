using GraphQL.Types;
using InventoryAPI.Queries;

namespace InventoryAPI.Schemas
{
    public class InventorySchema:Schema
    {
        public InventorySchema(IServiceProvider ServiceProvider)
        {
            Query = ServiceProvider.GetRequiredService<RootQuery>();
        }
    }
}
