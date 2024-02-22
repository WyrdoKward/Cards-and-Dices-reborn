using Assets._Scripts.Cards.Common;
using Assets._Scripts.Cards.Logic;
using Assets._Scripts.StateMachines.Cards.TimerState;
using UnityEngine;

/// <summary>
/// Extention class to switch state of other gameObjects outside of the state machine
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
        if (gameObject == null) return;

        var controller = gameObject.GetComponent<CardController>();
        var targetPosition = new Vector2(controller.LastPosition.x, controller.LastPosition.y);

        controller.UnlinkPreviousCard();
        controller.SwitchState(controller.MovingState);
        controller.currentMovementState.TargetPosition = targetPosition;
        //Debug.Log($"Sending {gameObject} from {gameObject.transform.position} to {controller.LastPosition}");
    }

    /// <summary>
    /// Déjà running => ne correspond plus => stop running 
    /// Déjà running => si receipe => relancer la nouvelle
    /// Pas running=> si receipe => lancer
    /// </summary>
    /// <returns>True si une recette correspond</returns>
    public static bool RunOrResetIfRecipe(this GameObject gameObject)
    {
        if (gameObject == null) return false;
        Debug.Log($"Run if receipe {gameObject}");
        var controller = gameObject.GetComponent<CardController>();
        var action = gameObject.GetComponent<CardLogic>().GetReceipe();

        //Arrête le timer en cours si il n'y a plus de recette
        if (action == null)
        {
            controller.SwitchState(controller.NoTimerState);
            return false;
        }

        if (gameObject.IsAlreadyRunning())
            controller.RunningState.ResetState(action);
        else
        {
            controller.RunningState.EndTimerAction = action;
            controller.SwitchState(controller.RunningState);
        }

        return true;
    }

    public static bool IsAlreadyRunning(this GameObject gameObject)
    {
        if (gameObject == null) return false;

        var controller = gameObject.GetComponent<CardController>();
        if (controller.currentTimerState is CardRunningState)
            return true;
        return false;
    }
}
