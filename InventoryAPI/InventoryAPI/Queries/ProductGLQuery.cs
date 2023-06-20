/*
using GraphQL;
using GraphQL.Types;
using InventoryAPI.Repositories;

namespace InventoryAPI.Queries
{
    public class ProductGLQuery : ObjectGraphType
    {
        public ProductGLQuery(IProductRepo productRepo)
        {
            Name = "Products";
            Field<ListGraphType<ProductGLType>>(
                "AllProducts",
                resolve: context => productRepo.GetProducts()
                );
            Field<ProductGLType>(
                "ProductById",
                 arguments: new QueryArguments(new QueryArgument<LongGraphType>
                 { Name = "productId" }),
                 resolve: context => productRepo.GetProductById(context.GetArgument<long>("productId"))
                );
        }
    }
}
*/