
namespace ConsoleApp
{
    public class Player : Character
    {
        public event Action<int>? OnPlayerHealed;
        public event Action OnBuffApplied;
        public event Action RemoveDamageBuff;
        private int _additionalDamage;
        private Weapon? _activeWeapon;
        private Potion? _activePotion;
        private int _potionDuration;
        private GameDirector _gameDirector;
        
        public Weapon? ActiveWeapon => _activeWeapon;
        public Potion? ActivePotion => _activePotion;
        

        public Player(string name, int health, int baseDamage)
        {
            Name = name;
            this.health = health;
            this.baseDamage = baseDamage;
        }

        public void SetGameDirector(GameDirector director)
        {
            _gameDirector = director;
        }
        public void SetActiveWeapon(Weapon weapon)
        {
            _activeWeapon?.Unequip(this);
            _activeWeapon = weapon;
            _activeWeapon.Equip(this);
        }

        public void SetActivePotion(Potion potion)
        {
            _activePotion = potion;
            _potionDuration = _activePotion?.Duration ?? 0;
        }

        public void UsePotion()
        {
            _activePotion?.Consume(this);
            _gameDirector.OnTurnEnded += PotionTick;
        }

        private void PotionTick()
        {
            if (_activePotion is null)
                return;

            _potionDuration--;

            if (_potionDuration >= 0)
            {
                _activePotion.Tick(this);
            }

            if (_potionDuration < 0)
            {
                _gameDirector.OnTurnEnded -= PotionTick;
                _activePotion.RemoveBuff(this);
                RemoveDamageBuff?.Invoke();
            }
        }

        public void Heal(int value)
        {
            health += value;
            OnPlayerHealed?.Invoke(value);
        }

        public void AddDamageBuff(int value)
        {
            _additionalDamage += value;
            OnBuffApplied?.Invoke();
        }

        public void AddAdditionalDamage(int value)
        {
            _additionalDamage += value;
        }
        
        public void RemoveAdditionalDamage(int value)
        {
            _additionalDamage -= value;
        }

        public override int Attack()
        {
            return baseDamage + _additionalDamage;
        }
    }

}


