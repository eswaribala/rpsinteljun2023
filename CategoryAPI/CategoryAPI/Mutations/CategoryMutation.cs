using GraphQL;
using GraphQL.Types;
using CategoryAPI.Models;
using CategoryAPI.Queries;
using CategoryAPI.Repositories;

namespace CategoryAPI.Mutations
{
    public class CategoryMutation:ObjectGraphType
    {
        private ICategoryRepo _categoryRepo;
       

        public CategoryMutation(ICategoryRepo categoryRepo)
        {
            this._categoryRepo = categoryRepo;
           

            Name = "CategoryMutation";

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


            FieldAsync<StringGraphType>("deleteCategory",
            arguments: new QueryArguments(

                new QueryArgument<LongGraphType> { Name = "categoryId" }
             

                ),

            resolve: async context =>
            {

                var categoryId = context.GetArgument<long>("categoryId");
             
                await DeleteCategory(categoryId);
                return $"CategoryId {categoryId} is successfully deleted";
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

        private async Task<bool> DeleteCategory(long categoryId)
        {
            if (categoryId <= 0)
                return false;
            else
            {
                return await _categoryRepo.DeleteCategory(categoryId);
            }
        }



      

    }
}
