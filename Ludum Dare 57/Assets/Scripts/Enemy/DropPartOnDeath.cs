using UnityEngine;

public class DropPartOnDeath : MonoBehaviour
{
    [SerializeField] GameObject partPrefab;

    void OnDestroy()
    {
        Instantiate(partPrefab, transform.position, Quaternion.identity);
    }
}
