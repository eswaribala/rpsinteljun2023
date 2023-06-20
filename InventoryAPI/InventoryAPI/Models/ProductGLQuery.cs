using GraphQL;
using GraphQL.Types;
using InventoryAPI.Queries;
using InventoryAPI.Repositories;

namespace InventoryAPI.Models
{
    public class ProductGLQuery:ObjectGraphType
    {
        public ProductGLQuery(IProductRepo productRepo) {
            Name = "Products";
            Field<ListGraphType<ProductGLType>>(
                "All Products",
                resolve: context => productRepo.GetProducts()
                );
            Field<ProductGLType>(
                "Product ById",
                 arguments: new QueryArguments(new QueryArgument<LongGraphType>
                 { Name = "productId" }),
                 resolve: context => productRepo.GetProductById(context.GetArgument<long>("productId"))
                ) ;
        }
    }
}
