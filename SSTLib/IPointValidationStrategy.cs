namespace SSTLib
{
    public interface IPointValidationStrategy
    {
        bool ValidateX(double value);
        bool ValidateY(double value);
    }
}
