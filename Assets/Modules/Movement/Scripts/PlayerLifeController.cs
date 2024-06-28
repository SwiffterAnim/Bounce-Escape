namespace Modules.Movement.Scripts
{
    public class PlayerLifeController
    {
        private int shieldCount = 3;

        private void OnDamageReceived(int damage)
        {
            if (shieldCount > 0)
            {
                shieldCount--;
            }
            else
            {
                // reduce the life
            }
            
            
        }
    }
}