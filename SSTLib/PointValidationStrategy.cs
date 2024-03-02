using System;

namespace SSTLib
{
    public class PointValidationStrategy : IPointValidationStrategy
    {
        private readonly double XLowConstraint;
        private readonly double XHighConstraint;

        private readonly double YLowConstraint;
        private readonly double YHighConstraint;
        public PointValidationStrategy(double xLowConstraint, double xHighConstraint, double yLowConstraint, double yHighConstraint)
        {
            XLowConstraint = xLowConstraint;    
            XHighConstraint = xHighConstraint;
            YLowConstraint = yLowConstraint;
            YHighConstraint = yHighConstraint;
        }
        public bool ValidateX(double x)
        {
            if (x < XLowConstraint || x > XHighConstraint)
            {
                throw new ArgumentException($"X value must be from {XLowConstraint} to {XHighConstraint}");
            }
            return true;
        }

        public bool ValidateY(double y)
        {
            if (y < YLowConstraint || y > YHighConstraint)
            {
                throw new ArgumentException($"Y value must be from {YLowConstraint} to {YHighConstraint}");
            }
            return true;
        }
    }
}
