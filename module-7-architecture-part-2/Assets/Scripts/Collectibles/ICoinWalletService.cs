using System;

public interface ICoinWalletService
{
    int Balance { get; }
    event Action<int> BalanceChanged;
    void AddCoins(int amount);
}
