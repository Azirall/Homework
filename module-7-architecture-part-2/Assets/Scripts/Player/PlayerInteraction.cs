using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IHealthService _healthService;
    private ICoinWalletService _coinWalletService;
    private ILoggerService _logger;

    public void Initialize(IHealthService healthService, ICoinWalletService coinWalletService, ILoggerService logger)
    {
        _healthService = healthService;
        _coinWalletService = coinWalletService;
        _logger = logger;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_coinWalletService != null && other.TryGetComponent(out ICoin coin))
        {
            _logger.ItemPickup();
            _logger.CoinCollected(coin.Value);
            _coinWalletService.AddCoins(coin.Value);

            if (other.TryGetComponent(out IPoolItem poolItem))
                poolItem.ReturnToPool();
        }

        if (_healthService == null)
            return;

        if (other.TryGetComponent(out IDamageDealer damageDealer))
        {
            _healthService.GetDamage(damageDealer.Damage);
        }

        if (other.TryGetComponent(out IHealDealer healDealer))
        {
            _healthService.GetHeal(healDealer.HealAmount);
            Destroy(other.gameObject);
        }
    }
}
