public class CoinWalletService : ICoinWalletService
{
    private int _balance;

    public void AddCoins(int amount)
    {
        _balance += amount;
    }
}
