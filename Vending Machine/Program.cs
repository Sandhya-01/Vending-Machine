using System;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;


namespace VendingMachine
{
    public class Machine
    {
        static Machine customer = new Machine();
        double currentamount;
        public double balance;
        int counter = 1;
        double coinvalue;
        double productamount;
        string input="0";
        public Machine()
        {
            currentamount = 0;
            balance = 0;
        }
        public void CalculateAmount(double amount)
        {
            customer.currentamount += amount;
            Console.WriteLine("Current Balance :" + customer.currentamount);
        }
        public void ValidateInsertedCoin(string weight, string size, out double coinvalue)
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
                    counter = 0;
                }
                
            }
            customer.DisplayValidMessage(customer.coinvalue);
        }
        public void DisplayValidMessage(double value)
        {
            if (value == 0 || value == null)
            {
                counter= 0;
                Console.WriteLine("Invalid Coin.Please collect your coin");
                Console.WriteLine("Current Balance :" + customer.currentamount);

            }
            else

                customer.CalculateAmount(value);


        }
        public void DisplayProductDetails()
        {
            Console.WriteLine("Choose from below available Products");
            Console.WriteLine("Press 1 for Cola $1");
            Console.WriteLine("Press 2 for Chips $0.5");
            Console.WriteLine("Press 3 for Candy $0.65");
            
        }
        public void GetCoinDetails()
        {
            Console.WriteLine("Please  Insert the Coin");
            Console.WriteLine("Note: input taken as weight of coin and size of the coin");
            string weight = Console.ReadLine();
            string size = Console.ReadLine();
            customer.ValidateInsertedCoin(weight, size, out customer.coinvalue);
            if(customer.currentamount!=0)
                customer.PurchaseProduct();
        }
        public void PurchaseProduct()
        {
            //if (customer.counter == 1)
            //{
                customer.DisplayProductDetails();
                int choosenProduct = int.Parse(Console.ReadLine());
                switch (choosenProduct)
                {
                    case 1:
                        Console.Write("Choosen Product is Cola,Please Insert coins ");
                        customer.balance = VendingMachineConfig.ColaPrice - customer.currentamount;
                        customer.productamount = VendingMachineConfig.ColaPrice;
                        break;
                    case 2:
                        Console.Write("Choosen Product is Chips,Please Insert coins ");
                        customer.balance = VendingMachineConfig.ChipsPrice - customer.currentamount;
                        customer.productamount = VendingMachineConfig.ChipsPrice;
                        break;
                    case 3:
                        Console.Write("Choosen Product is Candy,Please Insert coins ");
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
            while(customer.input!="1")
                customer.GetCoinDetails();
            while(customer.input=="1")
                customer.PurchaseProduct();
            
        }

        public void ProcessingBill(double balance)
        {
            while ( balance>0)
            {
                string weight = Console.ReadLine();
                string size = Console.ReadLine();
                customer.ValidateInsertedCoin(weight, size, out coinvalue);
                
                customer.balance = productamount - customer.currentamount;
                balance = customer.balance;
                if (customer.currentamount >= customer.productamount)
                {
                    Console.WriteLine("Please collect your Product");
                    customer.currentamount -= customer.productamount;
                    if (customer.currentamount == 0)
                    {
                        customer.input = "0";
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
                            Console.WriteLine("Please collect your balance");
                            break;
                        }
                    }
                   
                    
                }
                
                Console.WriteLine("Insert coins for :" + (customer.balance )); 
                 
                
            }

            

        }
    }
}
