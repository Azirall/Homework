using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private HealthService _healthService;
    private ICoinWalletService _coinWalletService;
    private ILoggerService _logger;

    public void Initialize(HealthService healthService, ICoinWalletService coinWalletService, ILoggerService logger)
    {
        _healthService = healthService;
        _coinWalletService = coinWalletService;
        _logger = logger;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_coinWalletService != null && other.TryGetComponent(out ICoin coin))
        {
            _logger.CoinCollected(coin.Value);
            _coinWalletService.AddCoins(coin.Value);
            Destroy(other.gameObject);
        }

        if (_healthService == null)
            return;

        if (other.TryGetComponent(out IDamageDealer damageDealer))
        {
            _logger.DamageReceived(damageDealer.Damage);
            _healthService.GetDamage(damageDealer.Damage);
        }

        if (other.TryGetComponent(out IHealDealer healDealer))
        {
            _logger.HealReceived(healDealer.HealAmount);
            _healthService.GetHeal(healDealer.HealAmount);
            Destroy(other.gameObject);
        }
    }
}
