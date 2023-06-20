using GraphQL;
using GraphQL.Types;
using InventoryAPI.Models;
using InventoryAPI.Queries;
using InventoryAPI.Repositories;

namespace InventoryAPI.Mutations
{
    public class RootMutation:ObjectGraphType
    {
        private ICategoryRepo _categoryRepo;
        private IProductRepo _productRepo;

        public RootMutation(ICategoryRepo categoryRepo,ProductRepo productRepo)
        {
            this._categoryRepo = categoryRepo;
            this._productRepo = productRepo;

            Name = "InventoryMutation";

            FieldAsync<CategoryGLType>("insertCategory",
            arguments: new QueryArguments(

                new QueryArgument<CategoryGLInputType> { Name = "category" }),

            resolve: async context =>
            {

                var category = context.GetArgument<Category>("category");

                var result = await InsertCategory(category);
                return result;
            });

            FieldAsync<CategoryGLType>("updateCategory",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "categoryId" },
                new QueryArgument<StringGraphType> { Name = "categoryName" }

                ),

            resolve: async context =>
            {

                var categoryId = context.GetArgument<long>("categoryId");
                var categoryName = context.GetArgument<string>("categoryName");
                var result = await UpdateCategory(categoryId,categoryName);
                return result;
            });


        }


        private async Task<Category> InsertCategory(Category Category)
        {
            if (Category == null)
                return null;
            else
            {
                return await _categoryRepo.AddCategory(Category);
            }
        }
        private async Task<Category> UpdateCategory(long categoryId, string categoryName)
        {
            if (categoryId <= 0)
                return null;
            else
            {
                return await _categoryRepo.UpdateCategory(categoryId, categoryName);
            }
        }

    }
}
