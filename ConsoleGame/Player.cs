
namespace ConsoleApp
{
    public class Player : Character
    {
  
        private Weapon _activeWeapon;
        private Potion _activePotion;
        private int _potionDuration;
        private GameDirector _gameDirector;
        public Player(string name, int health, int baseDamage)
        {
            Name = name;
            this.health = health;
            this.baseDamage = baseDamage;
            
        }

        public void SetGameDirector(GameDirector director)
        {
            _gameDirector = director;
            _gameDirector.OnTurnEnded += GetPotionEffect;
        }

        private void Heal(int heal)
        {
            health += heal;
        }
        public void ChangeWeapon(Weapon weapon)
        {
            _activeWeapon?.Unequip(this);
            _activeWeapon = weapon;
            _activeWeapon.Equip(this);
        }

        public void SetAdditionalDamage(int additionalDamage)
        {
            baseDamage += additionalDamage;
        }
        public void RemoveAdditionalDamage(int additionalDamage)
        {
            baseDamage -= additionalDamage;
        }

        public void ConsumePotion()
        {
            if (_potionDuration != 0) return;

            _potionDuration = _activePotion.Duration;
            if (_activePotion.AdditionalDamage > 0)
            {
                SetAdditionalDamage(_activePotion.AdditionalDamage);
            }
        }

        public void SetPotion(Potion potion)
        {
            _activePotion = potion;
        }

        private void GetPotionEffect()
        {
            if (_potionDuration > 0)
            {
                _potionDuration--;
                Heal(_activePotion.HealPower);
            }
            else
            {
                RemoveAdditionalDamage(_activePotion.AdditionalDamage);
            }
        }
    }
}


