namespace Assets._Scripts.StateMachines
{
    public interface IState
    {
        void Enter(IStateContext context);
        void UpdateState(IStateContext context);
        void Exit(IStateContext context);
    }
}
