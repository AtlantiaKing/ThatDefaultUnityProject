namespace That
{
    namespace FSM
    {
        public class FiniteStateMachine
        {
            public FiniteStateMachine(State startState)
            {
                _currentState = startState;
            }
            protected State _currentState;

            public virtual void Update<T>(T data)
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
