using System;

namespace ConsoleApp;

public class GameDirector
{
    public event Action OnTurnEnded;
    
    private bool _gameRunning = true;
    private Player _player;
    private Enemy _enemy;
    private ConsoleUI _consoleUI;
    private Inventory _inventory;
    public GameDirector(Player player, Enemy enemy,ConsoleUI consoleUi,Inventory inventory)
    {
        _inventory = inventory;
        _consoleUI  = consoleUi;
        _player = player;
        _enemy = enemy;


        _consoleUI.OnMenuChoice += HandleMenuChoice;
    }

    public void RunGame()
    {
        _consoleUI.ShowMenu();
    }

    private void HandleMenuChoice(int choice)
    {
        if (choice == 0)
        {
            _consoleUI.ShowMenu();
        }

        switch (choice)
        {
            case 1:
                Attack();
                break;
            case 2:
                UsePotion();
                break;
            case 3:
                ChangeWeapon();
                break;
            case 4:
                ChangePotion();
                break;
        }

    }
    
    private void Attack()
    {
        int playerDamage = _player.Attack();
        _enemy.TakeDamage(playerDamage);
        _consoleUI.ShowAttackResult(_enemy,playerDamage);
        
        int enemyDamage = _enemy.Attack();
        _player.TakeDamage(enemyDamage);
        _consoleUI.ShowAttackResult(_player,enemyDamage);
        
        OnTurnEnded?.Invoke();
        _consoleUI.ShowMenu();
    }
    private void UsePotion()
    {
        _player.ConsumePotion();
    }
    private void ChangeWeapon()
    {
        List<Weapon> weapons = _inventory.GetWeaponsList();
        _consoleUI.PrintItemList(weapons, out int choice);
        Weapon currentWeapon =  weapons[choice-1];
        _player.ChangeWeapon(currentWeapon);
        _consoleUI.ShowMenu();
    }
    private void ChangePotion()
    {
        List<Potion> potions = _inventory.GetPotionList();
        _consoleUI.PrintItemList(potions, out int choice);
        Potion currentPotion = potions[choice-1];
        _player.SetPotion(currentPotion);
        _consoleUI.ShowMenu();
    }
}

