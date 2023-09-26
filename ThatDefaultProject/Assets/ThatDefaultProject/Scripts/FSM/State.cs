namespace that
{
    namespace FSM
    {
        public abstract class State
        {
            public virtual void OnEnter() { }
            public virtual State OnHandle<T>(T data) { return this; }
            public virtual void OnExit() { }
        }
    }
}
