namespace WebApi.Validators
{
    public interface IValidationRule<T>
    {
        bool IsValid(T instance);
    }
}