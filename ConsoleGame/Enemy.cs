namespace ConsoleApp
{
    public class Enemy : Character
    {
        public Enemy(string name, int health, int baseDamage)
        {
            this.Name = name;
            this.health = health;
            this.baseDamage = baseDamage;
        }
    }
}


