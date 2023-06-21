using GraphQL.Types;

namespace ProductAPI.Mutations
{
    public class ProductGLInputType:InputObjectGraphType
    {
        public ProductGLInputType()
        {
            Name = "ProductInput";
            Field<NonNullGraphType<StringGraphType>>("ProductName");
            Field<NonNullGraphType<StringGraphType>>("SKU");
            Field<NonNullGraphType<ProductDescriptionGLInputType>>("productDescription");
        }
    }
}
