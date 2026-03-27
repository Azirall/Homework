
using System;

public class CoinWalletService : ICoinWalletService
{
    private int _balance;

    public int Balance => _balance;
    public event Action<int> BalanceChanged;

    public void AddCoins(int amount)
    {
        _balance += amount;
        BalanceChanged?.Invoke(_balance);
    }
}
