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
                 "AllCategories",                 
              resolve: context => categoryRepo.GetCategories()

                ) ;

            Field<CategoryGLType>(
                "CategoryById",
                 arguments: new QueryArguments(new QueryArgument<LongGraphType>
                 { Name = "categoryId" }),
                resolve: context => categoryRepo.GetCategoryById(context.GetArgument<long>("categoryId"))
               );
        
        
        }
    }
}
