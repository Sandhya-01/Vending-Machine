using System;
using System.Security.Cryptography.X509Certificates;

namespace VendingMachine
{
    public class Machine
    {
        static Machine customer = new Machine();
        public double currentamount;
        public double balance;
        public Machine()
        {
            currentamount = 0;
            balance = 0;
        }
        public void CalculateAmount(double amount)
        {
            currentamount = +amount;
        }
        public void ValidateInsertedCoin(string weight, string size, out double coinvalue)
        {


            if (weight == null || size == null || weight == "" || size == "")
            {
                coinvalue = -1;
                customer.DisplayValidMessage(coinvalue);

            }
            else
            {
                double coinweigh = double.Parse(weight);

                double coinsize = double.Parse(size);

                if (coinweigh == 1 && coinsize == 1)
                {
                    coinvalue = 0.05;

                }
                else if (coinweigh == 2 && coinsize == 2)
                {
                    coinvalue = 0.1;

                }
                else if (coinweigh == 3 && coinsize == 3)
                {
                    coinvalue = 0.25;

                }
                else
                    coinvalue = -1;

            }
        }
        public void DisplayValidMessage(double value)
        {
            if (value == -1 || value == null)
            {
                Console.WriteLine("Invalid Coin.Please collect your coin");
                Console.WriteLine("Current Balance :" + customer.currentamount);
            }

        }
        public void DisplayProductDetails()
        {
            Console.WriteLine("Choose from below available Products");
            Console.WriteLine("Press 1 for Cola $1");
            Console.WriteLine("Press 2 for Chips $0.5");
            Console.WriteLine("Press 3 for Candy $0.65");

        }
        public static void Main()
        {
            Console.WriteLine("Please  Insert the Coin");
            Console.WriteLine("Note: input taken as weight of coin and size of the coin");
            string weight = Console.ReadLine();
            string size = Console.ReadLine();

            customer.ValidateInsertedCoin(weight, size, out double coinvalue);
            customer.DisplayValidMessage(coinvalue);
            customer.CalculateAmount(coinvalue);
            Console.WriteLine("Current Balance" + customer.currentamount);
            customer.DisplayProductDetails();
            int choosenProduct = int.Parse(Console.ReadLine());
            switch (choosenProduct)
            {
                case 1:
                    Console.WriteLine("Choosen Product is Cola,Please Insert coins");
                    break;
                case 2:
                    Console.WriteLine("Choosen Product is Cola,Please Insert coins");
                    break;
                case 3:
                    Console.WriteLine("Choosen Product is Cola,Please Insert coins");
                    break;
                default:
                    Console.WriteLine("Enter Valid Product");
                    customer.DisplayProductDetails();
                    break;
            }
        }
    }
}
