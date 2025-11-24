using System;

namespace ConsoleApp;

public class ConsoleUI
{
    public event Action<int> OnMenuChoice;
    private bool _listening;

    public void ShowMenu()
    {
        Console.WriteLine("\n");
        Console.WriteLine("1) Атаковать противника");
        Console.WriteLine("2) Использовать зелье");
        Console.WriteLine("3) Выбрать оружие");
        Console.WriteLine("4) Выбрать зелье");
        Console.WriteLine("5) Показать статус");
        Console.WriteLine("0) Выход из игры");
        Console.WriteLine("\n");

        ReadMenuChoice();
    }

    private void ReadMenuChoice()
    {
        Console.WriteLine("\n");
        Console.Write("Выберете действие: ");
        Console.WriteLine("\n");

        int input = ReadInput();
        OnMenuChoice?.Invoke(input);
    }

    private int ReadInput()
    {
        string input = Console.ReadLine();
        if (int.TryParse(input, out int choice))
            return choice;

        Console.WriteLine("Некорректный ввод: " + input);
        return -1;
    }

    public void ShowAttackResult(Character target, int damage)
    {
        Console.WriteLine($"{target.Name} получил {damage} урона");
    }

    public void PrintItemList<T>(List<T> list, out int itemChoice)
    {
        if (list.Count == 0)
        {
            itemChoice = -1;
            return;
        }

        Console.WriteLine("\n");
        Console.WriteLine("0) Выход");

        if (list is List<Weapon> weapons)
        {
            for (int j = 0; j < list.Count; j++)
            {
                Console.WriteLine($"{j + 1}) {weapons[j].Name} | Цена:{weapons[j].Price} | Урон:{weapons[j].AdditionalDamage}");
            }

            Console.WriteLine("Выберете оружие");
            itemChoice = ReadInput();

            if (itemChoice > 0 && itemChoice <= weapons.Count)
            {
                Console.WriteLine($"Выбрано оружие: {weapons[itemChoice - 1].Name}");
                return;
            }
        }

        if (list is List<Potion> potions)
        {
            for (int j = 0; j < list.Count; j++)
            {
                Console.WriteLine($"{j + 1}) {potions[j].Name} | Лечение:{potions[j].HealPower} | Урон:{potions[j].AdditionalDamage} | Длительность:{potions[j].Duration}");
            }

            Console.WriteLine("Выберете зелье");
            itemChoice = ReadInput();

            if (itemChoice > 0 && itemChoice <= potions.Count)
            {
                Console.WriteLine($"Выбрано зелье: {potions[itemChoice - 1].Name}");
                return;
            }
        }

        itemChoice = 0;
        ShowMenu();
    }
}

