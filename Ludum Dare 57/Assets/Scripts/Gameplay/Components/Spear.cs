public class Spear : Projectile, IInteractable
{
    void IInteractable.Interact(Player player)
    {
        player.SpearHandling.Retrieve();
    }
}