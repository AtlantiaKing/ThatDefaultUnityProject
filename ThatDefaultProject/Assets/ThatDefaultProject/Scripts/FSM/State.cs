namespace That
{
    namespace FSM
    {
        public interface IState<T>
        {
            public virtual void OnEnter(T data = default) { }
            public virtual IState<T> OnHandle(T data = default) { return this; }
            public virtual void OnExit(T data = default) { }
        }
    }
}
