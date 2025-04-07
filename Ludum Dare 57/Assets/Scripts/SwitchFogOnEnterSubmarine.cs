using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class SwitchFogOnEnterSubmarine : MonoBehaviour
{
    [SerializeField] LocalVolumetricFog fog1;
    [SerializeField] LocalVolumetricFog fog2;

    void OnEnable()
    {
        GameEvents.OnPlayerEnterSubmarine += SwitchFog;
    }

    void OnDisable()
    {
        GameEvents.OnPlayerEnterSubmarine -= SwitchFog;
    }
    
    void SwitchFog()
    {
        Destroy(fog1);
        fog2.enabled = true;
    }
}
