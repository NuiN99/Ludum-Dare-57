using System;

public static class GameEvents
{
    public static event Action<bool> OnAimStateChanged = delegate { };
    public static void InvokeAimStateChanged(bool isAiming) => OnAimStateChanged.Invoke(isAiming);
    
    public static event Action<bool> OnPlayerHoldingPartStateChanged = delegate { };
    public static void InvokePlayerHoldingPartStateChanged(bool isHoldingPart) => OnPlayerHoldingPartStateChanged.Invoke(isHoldingPart);
    
    public static event Action<Part.Type> OnPartRepaired = delegate { };
    public static void InvokePartRepaired(Part.Type partType) => OnPartRepaired.Invoke(partType);

    public static event Action<bool> OnLeviathanActiveStateChanged = delegate { };
    public static void InvokeLeviathanActiveStateChanged(bool isActive) => OnLeviathanActiveStateChanged.Invoke(isActive);
    
    public static event Action OnPlayerEnterSubmarine = delegate { };
    public static void InvokePlayerEnterSubmarine() => OnPlayerEnterSubmarine.Invoke();
    
    public static event Action OnPlayerDied = delegate { };
    public static void InvokePlayerDied() => OnPlayerDied.Invoke();
}
