using UnityEngine;

public class DropPartOnDeath : MonoBehaviour
{
    [SerializeField] GameObject partPrefab;
    bool _isQuitting;
    
    void OnEnable()
    {
        Application.quitting += SetIsQuitting;
    }
    void OnDisable()
    {
        Application.quitting -= SetIsQuitting;
    }

    void SetIsQuitting()
    {
        _isQuitting = true;
    }

    void OnDestroy()
    {
        if (_isQuitting)
        {
            return;
        }
        
        Instantiate(partPrefab, transform.position, Quaternion.identity);
    }
}
