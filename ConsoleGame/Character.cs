namespace ConsoleApp
{
    public abstract class Character
    {
        public string Name { get; protected set; }
        protected int health;
        protected int baseDamage;



        public virtual int Attack()
        {
            return baseDamage;
        }

        public virtual void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0)
            {
                health = 0;
            }
        }
    }
}


