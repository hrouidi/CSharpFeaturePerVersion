namespace CSharp11
{
    public class GenericMath
    {

    }


    public interface ICalc<T>  where T : ICalc<T>
    {
        public static abstract T operator +(T left, T right);

        public static virtual T operator ++(T self) => self; 
    }
}