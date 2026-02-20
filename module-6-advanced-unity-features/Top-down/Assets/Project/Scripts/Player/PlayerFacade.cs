public class PlayerFacade
{
        private PlayerController _playerController;
        private PlayerHealth _playerHealth; 
        private PlayerGun _playerGun;
   
        public PlayerFacade(PlayerController playerController, PlayerGun playerGun, PlayerHealth playerHealth)
        {
                _playerController = playerController;
                _playerHealth = playerHealth;
                _playerGun = playerGun;
        }
}