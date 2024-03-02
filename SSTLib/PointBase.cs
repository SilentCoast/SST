namespace SSTLib
{
    public class PointBase
    {
        private double x;
        private double y;
        private readonly IPointValidationStrategy ValidationStrategy;
        public PointBase(double x, double y, IPointValidationStrategy validationStrategy)
        {
            ValidationStrategy = validationStrategy;
            X = x;
            Y = y;
        }
        public double X
        {
            get => x; set
            {
                ValidationStrategy.ValidateX(value);
                x = value;
            }
        }
        public double Y
        {
            get => y; set
            {
                ValidationStrategy.ValidateY(value);
                y = value;
            }
        }
    }
}
