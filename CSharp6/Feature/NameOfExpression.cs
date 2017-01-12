using System;

namespace CSharp6.Feature
{
    class NameOfExpression
    {
        public void Do(int renameMePlease)
        {
            if(renameMePlease < 0)
                throw new ArgumentException(nameof(renameMePlease));
        }

        // Very usuful for WPF INotifyPropertyChanged pattern
    }
}
