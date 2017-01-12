namespace CSharp6.Feature
{
    class ExpressionBodiedFunctions
    {
        // Methods
        private  double AddNumbers(double num1, double num2)=> num1 + num2;


        // Property
        public string Name => "PoPo";

        //Index
        public string this[int index] => Name;


        //Operator
        public static ExpressionBodiedFunctions operator ++(ExpressionBodiedFunctions c1) => c1;
    }
}
