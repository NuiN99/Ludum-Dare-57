using UnityEngine;

public class Spear : Projectile, IInteractable
{
    public struct Data
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;
        
        public Data(Vector3 position, Quaternion rotation, Vector3 velocity)
        {
            this.position = position;
            this.rotation = rotation;
            this.velocity = velocity;
        }
    }
    
    void IInteractable.Interact(Player player)
    {
        player.SpearHandling.Retrieve();
    }
}