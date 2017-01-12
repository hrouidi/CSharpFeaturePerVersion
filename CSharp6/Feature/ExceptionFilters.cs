using System;

namespace CSharp6.Feature
{
    class ExceptionFilters
    {
        public void Do()
        {
            #region Prior to C# 6.0

            var httpStatusCode = 404;
            Console.Write("HTTP Error: ");

            try
            {
                throw new Exception(httpStatusCode.ToString());
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("500"))
                    Console.Write("Bad Request");
                else if (ex.Message.Equals("401"))
                    Console.Write("Unauthorized");
                else if (ex.Message.Equals("402"))
                    Console.Write("Exception Occurred");
                else if (ex.Message.Equals("403"))
                    Console.Write("Forbidden");
                else if (ex.Message.Equals("404"))
                    Console.Write("Not Found");
            }

            Console.ReadLine();

            #endregion

            Console.Write("HTTP Error: ");

            try
            {
                throw new Exception(httpStatusCode.ToString());
            }
            catch (Exception ex) when (ex.Message.Equals("400"))
            {
                Console.Write("Bad Request");
            }
            catch (Exception ex) when (ex.Message.Equals("401"))
            {
                Console.Write("Unauthorized");
            }
            catch (Exception ex) when (ex.Message.Equals("402"))
            {
                Console.Write("Exception Occurred ");
            }
            catch (Exception ex) when (ex.Message.Equals("403"))
            {
                Console.Write("Forbidden");
            }
            catch (Exception ex) when (ex.Message.Equals("404"))
            {
                Console.Write("Not Found");
            }

            Console.ReadLine();
        }
    }
}
