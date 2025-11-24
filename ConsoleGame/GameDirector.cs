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

        
    }

    public void RunGame()
    {
        SubscribeInEvent();
        _consoleUI.ShowMenu();
        
        while (true)
        {
            GameLoop();
        }
    }

    private void GameLoop()
    {
        int input = _consoleUI.ReadInput();
        
        switch (input)
        {
            case 1: Attack();
                break;
            case 2: UsePotion();
                break;
            case 3: ChangeWeapon();
                break;
            case 4: ChangePotion();
                break;
        }
        _consoleUI.ShowMenu();
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
    }
    private void UsePotion()
    {
        _player.UsePotion();
        OnTurnEnded?.Invoke();
    }
    private void ChangeWeapon()
    {
        List<Weapon> weapons = _inventory.GetWeaponsList();
        _consoleUI.PrintItemList(weapons, out int choice);
        Weapon currentWeapon =  weapons[choice-1];
        _player.SetActiveWeapon(currentWeapon);
    }
    private void ChangePotion()
    {
        List<Potion> potions = _inventory.GetPotionList();
        _consoleUI.PrintItemList(potions, out int choice);
        Potion currentPotion = potions[choice-1];
        _player.SetActivePotion(currentPotion);
    }

    private void SubscribeInEvent()
    {
        _player.OnPlayerHealed += HandlePlayerHealth;
        _player.RemoveDamageBuff += HandleRemoveBuff;
    }

    private void HandlePlayerHealth(int health)
    {
        _consoleUI.ShowHealMessage(health);
    }

    private void HandleRemoveBuff()
    {
        _consoleUI.ShowRemoveBuffMessage();
    }
}