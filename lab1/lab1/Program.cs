using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class Product
{
    public string _Name { get; set; }
    public int _Price { get; set; }
    public int _Amount { get; set; }

    public Product(string Name, int Price, int Amount)
    {
        _Name = Name;
        _Price = Price;
        _Amount = Amount;
    }
}

class VendingMachine
{
    static List<int> depositedCoins = new List<int>();
    static List<Product> Products = new List<Product>()
    {
        new Product("Pepsi", 45, 10),
        new Product("Fanta", 50, 15),
        new Product("Dr. Pepper", 85, 20),
        new Product("Coca-Cola", 50, 10),
        new Product("Lipton", 35, 15)
    };
    static int machineWallet = 0;
    static int userWallet = 0;

    private static readonly int[] AvailableCoins = { 1, 2, 5, 10 };

    static void Main(string[] args)
    {
        Console.WriteLine("Вендинговый автомат.");

        while (true)
        {
            DisplayMenu();
            string operation = Console.ReadLine();

            switch (operation)
            {
                case "1":
                    DisplayProducts();
                    break;
                case "2":
                    DepositeMoney();
                    break;
                case "3":
                    BuyProduct();
                    break;
                case "4":
                    CancelOperatin();
                    break;
                case "5":
                    EnterAdminMode();
                    break;
                default:
                    Console.WriteLine("Такой операции нет.");
                    break;
            }
        }
    }
    static void DisplayMenu()
    {
        Console.WriteLine("1. Посмотреть список товаров");
        Console.WriteLine("2. Внести деньги");
        Console.WriteLine("3. Выбрать и купить товар");
        Console.WriteLine("4. Отменить операцию");
        Console.WriteLine("5. Войти в администраторский режим");
        Console.Write("Выберите действие: ");
    }
    static void DisplayProducts()
    {
        Console.WriteLine("Товары: ");
        for (int i = 0; i < Products.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Products[i]._Name}, {Products[i]._Price} руб., {Products[i]._Amount} шт.");
        }
    }
    static void DepositeMoney()
    {
        /*
        Console.WriteLine("Для оплаты вы можете воспользоваться монетами номиналоми 1, 2, 5, 10 руб.");
        Console.Write("Внесите деньги: ");
        string inputCoins = Console.ReadLine();
        string[] AvailableCoins = { "1", "2", "5", "10" };

        if (AvailableCoins.Contains(inputCoins))
        {
            int Coins = int.Parse(inputCoins);
            userWallet += Coins;
            depositedCoins.Add(Coins);
            Console.WriteLine($"Внесено: {Coins} руб., Баланс: {userWallet} руб.");
        }
        else
        {
            Console.WriteLine("Внесите деньги c подходящим номиналом.");
        }
        */
        Console.WriteLine($"Доступные номиналы: {string.Join(", ", AvailableCoins)} руб.");

        Console.Write("Внесите деньги: ");
        string input = Console.ReadLine();
        if (int.TryParse(input, out int coins) && AvailableCoins.Contains(coins))
        {
            userWallet += coins;
            depositedCoins.Add(coins);
            Console.WriteLine($"Внесено: {coins} руб. Баланс: {userWallet} руб.");
        }
        else
        {
            Console.WriteLine("Неверный номинал. Используйте только доступные монеты.");
        }
    }

    static void BuyProduct()
    {
        DisplayProducts();
        Console.WriteLine("Введите номер выбранного товара (0 для отмены операции): ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int result))
        {
            if (result == 0)
            {
                return;
            }
            if (0 < result && result <= Products.Count)
            {
                Product chosen = Products[result - 1];
                if (chosen._Amount == 0)
                {
                    Console.WriteLine("Товар закончился.");
                    return;
                }
                if (userWallet >= chosen._Price)
                {
                    Console.WriteLine($"Вы купили данный товар: {chosen._Name}");
                    chosen._Amount--;
                    machineWallet += chosen._Price;
                    userWallet -= chosen._Price;

                    if (userWallet > 0)
                    {
                        Console.WriteLine($"Сдача: {userWallet} руб.");
                        userWallet = 0;
                    }
                    depositedCoins.Clear();
                }
                else
                {
                    Console.WriteLine("Недостаточно срдеств.");
                }
            }
            else
            {
                Console.WriteLine("Неверный номер.");
            }
        }
        else
        {
            Console.WriteLine("Неверынй номер.");
        }

    }

    static void CancelOperatin()
    {
        if (depositedCoins.Count == 0 && userWallet == 0)
        {
            Console.WriteLine("Нет средств для возврата.");
            return;
        }
        int totalReturn = userWallet;
        Console.WriteLine($"Вернули {totalReturn} руб.");
        userWallet = 0;
        depositedCoins.Clear();
    }

    static void EnterAdminMode()
    {
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();
        if (password != "12345")
        {
            Console.WriteLine("Неверный пароль.");
            return;
        }
        while (true)
        {
            Console.WriteLine("1. Пополнить ассортимент товаров.");
            Console.WriteLine("2. Изъять деньги.");
            Console.WriteLine("3. Выйти из административного режима.");

            string inputOperation = Console.ReadLine();

            switch (inputOperation)
            {
                case "1":
                    AddNewProduct();
                    break;
                case "2":
                    Console.WriteLine($"Изъято {machineWallet} руб.");
                    machineWallet = 0;
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }

    static void AddNewProduct()
    {
        Console.Write("Введите название товара: ");
        string newName = Console.ReadLine();

        Console.Write("Введите цену: ");
        int Price = int.Parse(Console.ReadLine());

        Console.Write("Введите количество: ");
        int Amount = int.Parse(Console.ReadLine());

        Products.Add(new Product(newName, Price, Amount));
        Console.WriteLine("Товар успешно добавлен!");
    }
}



