namespace Assets._Scripts.StateMachines
{
    public interface IStateContext
    {
        void SwitchState(IState newState);
    }
}
