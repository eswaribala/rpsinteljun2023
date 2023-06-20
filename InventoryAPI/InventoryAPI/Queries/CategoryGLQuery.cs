using GraphQL;
using GraphQL.Types;
using InventoryAPI.Repositories;

namespace InventoryAPI.Queries
{
    public class CategoryGLQuery:ObjectGraphType
    {
        public CategoryGLQuery(ICategoryRepo categoryRepo) {

            Name = "Categories";
            Field<ListGraphType<CategoryGLType>>(
                 "All Categories",                 
              resolve: context => categoryRepo.GetCategories()

                ) ;

            Field<CategoryGLType>(
                "Category ById",
                 arguments: new QueryArguments(new QueryArgument<LongGraphType>
                 { Name = "categoryId" }),
                resolve: context => categoryRepo.GetCategoryById(context.GetArgument<long>("categoryId"))
               );
        
        
        }
    }
}
