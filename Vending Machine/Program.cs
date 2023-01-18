using System;
//using Constraintset =  double;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;


namespace VendingMachine
{
    public class Machine
    {
        static Machine customer = new Machine();
        double _currentamount;
        double _balance;
        
        public double currentamount
        {
            get
            {
                return _currentamount;

            }
            set { _currentamount = Math.Round(value, 2); }
        }
        public double balance
        {
            get { return _balance; }
            set { _balance = Math.Round(value, 2); }
        }

        double coinvalue;
        double productamount;
        string? input = "0";
        public Machine()
        {
            currentamount = 0;
            balance = 0;
        }
        public void CalculateAmount(double amount)
        {
            customer.currentamount += amount;
            //Console.WriteLine("Current Balance :" + customer.currentamount);
        }
        public void ValidateInsertedCoin(string? weight, string? size, out double coinvalue)
        {


            if (weight == null || size == null || weight == "" || size == "")
            {
                coinvalue = 0;


            }
            else
            {
                double coinweigh = double.Parse(weight);

                double coinsize = double.Parse(size);

                if (coinweigh == VendingMachineConfig.NickelWeight && coinsize == VendingMachineConfig.NickelSize)
                {
                    coinvalue = VendingMachineConfig.NickelValue;

                }
                else if (coinweigh == VendingMachineConfig.DimeWeight && coinsize == VendingMachineConfig.DimeSize)
                {
                    coinvalue = VendingMachineConfig.DimeValue;

                }
                else if (coinweigh == VendingMachineConfig.QuarterWeight && coinsize == VendingMachineConfig.QuarterSize)
                {
                    coinvalue = VendingMachineConfig.QuarterValue;

                }
                else
                {
                    coinvalue = 0;

                }

            }
            customer.DisplayValidMessage(customer.coinvalue);
        }
        public void DisplayValidMessage(double value)
        {
            if (value == 0)
            {

                Console.WriteLine("Invalid Coin! Please collect your coin");
                Console.WriteLine("Current Balance :" + customer.currentamount);

            }
            else

                customer.CalculateAmount(value);


        }
        public void DisplayProductDetails()
        {
            Console.WriteLine("Choose from below available Products");
            Console.WriteLine("Press 1 for Cola $" + VendingMachineConfig.ColaPrice);
            Console.WriteLine("Press 2 for Chips $" + VendingMachineConfig.ChipsPrice);
            Console.WriteLine("Press 3 for Candy $" + VendingMachineConfig.CandyPrice);

        }
        public void GetCoinDetails()
        {

            Console.WriteLine("**************WELCOME**************");
            Console.WriteLine("Please Insert the Coin");
            Console.WriteLine("Note: input taken as weight of coin and size of the coin one by one");
            var weight = Console.ReadLine();
            var size = Console.ReadLine();
            customer.ValidateInsertedCoin(weight, size, out customer.coinvalue);
            if (customer.currentamount != 0)
                customer.PurchaseProduct();
        }
        public void PurchaseProduct()
        {
            customer.DisplayProductDetails();
            var product = Console.ReadLine();
            var choosenProduct = int.Parse(product == "" ? "0" : product);
            switch (choosenProduct)
            {
                case 1:
                    Console.Write("Chosen Product is Cola ");
                    customer.balance = VendingMachineConfig.ColaPrice - customer.currentamount;
                    customer.productamount = VendingMachineConfig.ColaPrice;
                    break;
                case 2:
                    Console.Write("Chosen Product is Chips ");
                    customer.balance = VendingMachineConfig.ChipsPrice - customer.currentamount;
                    customer.productamount = VendingMachineConfig.ChipsPrice;
                    break;
                case 3:
                    Console.Write("Chosen Product is Candy ");
                    customer.balance = VendingMachineConfig.CandyPrice - customer.currentamount;
                    customer.productamount = VendingMachineConfig.CandyPrice;
                    break;
                default:
                    Console.WriteLine("Enter Valid Product");
                    customer.DisplayProductDetails();
                    break;
            }
            Console.WriteLine("for the amount: " + customer.balance);
            customer.ProcessingBill(customer.balance);


        }
        public static void Main()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configpath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(configpath, true);
            var root = configurationBuilder.Build();
            VendingMachineConfig appconfig = root.GetSection("VendingMachineConfig").Get<VendingMachineConfig>();
            while (customer.input != "1")
            {
                if (customer.input == "2")
                {
                    customer.PurchaseProduct();

                }
                else if (customer.input == "3")
                {
                    Console.WriteLine("Please collect your balance $"+customer.currentamount);
                    customer.currentamount = 0;
                    Thread.Sleep(3000);
                    Console.Clear();

                    customer.GetCoinDetails();
                }
                else if (customer.input == "0")
                    customer.GetCoinDetails();
                while (customer.input == "1")
                    customer.PurchaseProduct();

            }

        }

        public void ProcessingBill(double balance)
        {
            //bool flag = true;
            {
                while (balance >0 )
                {
                    Console.WriteLine("Press 2 to cancel item and go back to menu");
                    Console.WriteLine("Press 3 to cancel item and get the balance");
                    Console.WriteLine("Press any other key to continue current purchase");
                    int val = customer.CancelorChangeItem(int.Parse(Console.ReadLine()));
                    if (val != 1) { break; }
                    else if (val == 1)
                    {
                        Console.WriteLine("Please Insert coins for the amount $" + customer.balance);
                        Console.WriteLine("Note: input taken as weight of coin and size of the coin one by one");
                        Console.WriteLine("Please click Enter if finished loading coins");
                        while (customer.input != "Enter")
                        {
                            var weight = Console.ReadLine();
                            if (weight == "Enter")
                            {
                                customer.input = "Enter";
                                //flag = false;
                                break;
                            }

                            else
                            {
                                var size = Console.ReadLine();

                                customer.ValidateInsertedCoin(weight, size, out coinvalue);
                            }
                        }
                        Console.WriteLine("Current Balance: $" + customer.currentamount);

                        customer.balance = Math.Abs(productamount - customer.currentamount);
                        balance = customer.balance;
                        if (customer.currentamount >= customer.productamount)
                        {
                            Console.WriteLine("Please collect your Product");
                            Console.WriteLine("**************THANK YOU**************");
                            
                            
                            customer.currentamount -= customer.productamount;

                                if (customer.currentamount == 0)
                            {
                                customer.input = "0";
                                Thread.Sleep(3000);
                                Console.Clear();
                                break;
                            }
                            else if (customer.currentamount > 0)
                            {
                                Console.WriteLine("Current balance is :" + customer.currentamount);
                                Console.WriteLine("To Continue your shopping, Press 1");
                                Console.WriteLine("To Exit and collect balance amount, Press any key except 1");
                                customer.input = Console.ReadLine();
                                if (customer.input != "1")
                                {
                                    Console.WriteLine("Please collect your balance $"+customer.balance);
                                    Thread.Sleep(3000);
                                    Console.Clear();


                                    break;
                                }
                                break;
                            }


                        }

                        Console.WriteLine("Insert coins for :" + customer.balance);
                        
                    }
                    

                }


            }
        }
        public int CancelorChangeItem(int i)
        {
            if (i == 2)
            {
                customer.input = "2";
                return 0;
            }
            else if (i == 3)
            {
                customer.input = "3";
                return -1;

            }
            else
            {
                customer.input = "1";
                return 1;
            }
        }
    }
}
