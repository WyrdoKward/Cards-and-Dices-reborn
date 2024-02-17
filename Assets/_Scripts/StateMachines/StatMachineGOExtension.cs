using Assets._Scripts.Cards.Common;
using Assets._Scripts.Cards.Logic;
using UnityEngine;

/// <summary>
/// Extention method to swith state of other gameObjects outside of the state machine
/// </summary>
public static class StatMachineGOExtension
{
    public static void Idle(this GameObject gameObject)
    {
        if (gameObject == null) return;

        var controller = gameObject.GetComponent<CardController>();
        controller.SwitchState(controller.IdleState);
    }

    public static void Follow(this GameObject gameObject)
    {
        if (gameObject == null) return;

        var controller = gameObject.GetComponent<CardController>();
        controller.SwitchState(controller.FollowingState);
    }

    public static void Disperse(this GameObject gameObject)
    {
        var controller = gameObject.GetComponent<CardController>();
        var targetPosition = new Vector2(controller.LastPosition.x, controller.LastPosition.y);

        controller.UnlinkPreviousCard();
        controller.SwitchState(controller.MovingState);
        controller.currentMovementState.TargetPosition = targetPosition;
        //Debug.Log($"Sending {gameObject} from {gameObject.transform.position} to {controller.LastPosition}");
    }

    public static bool RunIfRecipe(this GameObject gameObject)
    {
        var controller = gameObject.GetComponent<CardController>();
        var action = gameObject.GetComponent<CardLogic>().GetReceipe();

        if (action == null)
        {
            controller.RunningState.EndTimerAction = null;
            return false;
        }

        controller.RunningState.EndTimerAction = action;
        controller.SwitchState(controller.RunningState);

        return true;
    }
}
