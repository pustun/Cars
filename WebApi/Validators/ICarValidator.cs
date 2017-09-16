using Model;

namespace WebApi.Validators
{
    public interface ICarValidator
    {
        bool IsValid(Car car);
    }
}