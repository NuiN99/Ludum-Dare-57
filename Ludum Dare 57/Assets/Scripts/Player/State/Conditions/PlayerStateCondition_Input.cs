using NuiN.NExtensions;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "ScriptableObjects/Player/State/Conditions/Input")]
public class PlayerStateCondition_Input : Condition<Player>
{
    public enum InputType
    {
        Move,
        Aim,
        Attack,
        Dash,
        Radar,
        Interact,
    }
    public enum PressType
    {
        IsPressed,
        IsNotPressed,
        WasPressedThisFrame,
        WasReleasedThisFrame,
    }
    
    [SerializeField] InputType input;
    [SerializeField] PressType pressType;
    
    protected override bool IsConditionMet(Player context)
    {
        return input switch
        {
            InputType.Move => EvaluateInput(InputManager.Controls.Actions.Move),
            InputType.Aim => EvaluateInput(InputManager.Controls.Actions.Aim),
            InputType.Attack => EvaluateInput(InputManager.Controls.Actions.Attack),
            InputType.Dash => EvaluateInput(InputManager.Controls.Actions.Dash),
            InputType.Radar => EvaluateInput(InputManager.Controls.Actions.Radar),
            InputType.Interact => EvaluateInput(InputManager.Controls.Actions.Interact),
            _ => false
        };
    }
    
    bool EvaluateInput(InputAction inputAction)
    {
        return pressType switch
        {
            PressType.IsPressed => inputAction.IsPressed(),
            PressType.IsNotPressed => !inputAction.IsPressed(),
            PressType.WasPressedThisFrame => inputAction.WasPressedThisFrame(),
            PressType.WasReleasedThisFrame => inputAction.WasReleasedThisFrame(),
            _ => false
        };
    }
}