namespace That
{
    namespace FSM
    {
        public class FiniteStateMachine<T>
        {
            public FiniteStateMachine(IState<T> startState)
            {
                _currentState = startState;
            }
            protected IState<T> _currentState;

            public virtual void Update(T data = default)
            {
                var previousState = _currentState;
                _currentState = _currentState.OnHandle(data);
                if (previousState != _currentState)
                {
                    previousState.OnExit();
                    _currentState.OnEnter();
                }
            }
        }
    }
}
