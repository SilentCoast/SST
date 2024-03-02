namespace SSTLib
{
    public class ThePoint : PointBase
    {
        public static double XLowConstraint => -10;
        public static double XHighConstraint => 10;
        public static double YLowConstraint => -10;
        public static double YHighConstraint => 10;
        public ThePoint(double x, double y) : base(x, y, 
            new PointValidationStrategy(XLowConstraint,XHighConstraint,YLowConstraint,YHighConstraint))
        {
            X = x;
            Y = y;
        }
    }
}
