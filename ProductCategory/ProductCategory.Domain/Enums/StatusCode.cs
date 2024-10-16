namespace ProductCategory.Domain.Enums
{
    public enum StatusCode
    {
        CategoryAlreadyExists = 1,
        CategoryNotFound = 2,

        Success = 200,
        InternalServerError = 500,
    }
}
