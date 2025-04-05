using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Controls Controls { get; private set; }

    public static Vector2 MoveInput => Controls.Actions.Move.ReadValue<Vector2>();
    public static Vector2 RotateInput => Controls.Actions.Rotate.ReadValue<Vector2>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        Controls = new Controls();
        Controls.Enable();
        Application.quitting += DisableControls;
    }

    static void DisableControls()
    {
        Controls?.Disable();
        Application.quitting -= DisableControls;
    }
}