using System;

public static class GameEvents
{
    public static event Action<bool> OnAimStateChanged = delegate { };
    public static void InvokeAimStateChanged(bool isAiming) => OnAimStateChanged.Invoke(isAiming);
    
    public static event Action<bool> OnPlayerHoldingPartStateChanged = delegate { };
    public static void InvokePlayerHoldingPartStateChanged(bool isHoldingPart) => OnPlayerHoldingPartStateChanged.Invoke(isHoldingPart);
}
