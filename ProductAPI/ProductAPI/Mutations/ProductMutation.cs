using GraphQL;
using GraphQL.Types;
using ProductAPI.Models;
using ProductAPI.Queries;
using ProductAPI.Repositories;

namespace ProductAPI.Mutations
{
    public class ProductMutation:ObjectGraphType
    {
      
        private IProductRepo _productRepo;

        public ProductMutation(IProductRepo productRepo)
        {
            
            this._productRepo = productRepo;

            Name = "ProductMutation";

           

            //product


            FieldAsync<ProductGLType>("insertProduct",
            arguments: new QueryArguments(

                new QueryArgument<ProductGLInputType> { Name = "product" },
                new QueryArgument<LongGraphType> { Name = "categoryId" }

                ),

            resolve: async context =>
            {

                var product = context.GetArgument<Product>("product");
                var categoryId = context.GetArgument<long>("categoryId");
                var result = await InsertProduct(categoryId,product);
                return result;
            });

            FieldAsync<ProductGLType>("updateProduct",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "productId" },
                new QueryArgument<StringGraphType> { Name = "productName" }

                ),

            resolve: async context =>
            {

                var productId = context.GetArgument<long>("productId");
                var productName = context.GetArgument<string>("productName");
                var result = await UpdateProduct(productId, productName);
                return result;
            });


            FieldAsync<StringGraphType>("deleteProduct",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "productId" }


                ),

            resolve: async context =>
            {

                var productId = context.GetArgument<long>("productId");

                await DeleteProduct(productId);
                return $"ProductId {productId} is successfully deleted";
            });



        }


       



        private async Task<Product> InsertProduct(long categoryId, Product product)
        {
            if (categoryId <=0 )
                return null;
            else
            {
                return await _productRepo.AddProduct(product, categoryId);  
            }
        }

        private async Task<Product> UpdateProduct(long productId, string productName)
        {
            if (productId <= 0)
                return null;
            else
            {
                return await _productRepo.UpdateProduct(productId, productName);
            }
        }


        private async Task<bool> DeleteProduct(long productId)
        {
            if (productId <= 0)
                return false;
            else
            {
                return await _productRepo.DeleteProduct(productId);
            }
        }

    }
}
