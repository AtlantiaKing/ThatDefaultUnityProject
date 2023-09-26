using System;
using System.Collections.Generic;

namespace that
{
    namespace BT
    {
        public abstract class Behavior
        {
            public abstract BehaviorState Execute(Blackboard blackboard);
            protected BehaviorState _currentState = BehaviorState.Failure;
        }

        public abstract class BehaviorComposite : Behavior
        {
            protected List<Behavior> _childBehaviors;

            public BehaviorComposite(List<Behavior> childBehaviors)
            {
                _childBehaviors = childBehaviors;
            }
        }

        #region COMPOSITES
        public class BehaviorSelector : BehaviorComposite
        {
            public BehaviorSelector(List<Behavior> childBehaviors) : base(childBehaviors)
            {
            }

            public override BehaviorState Execute(Blackboard blackboard)
            {
                foreach (var child in _childBehaviors)
                {
                    _currentState = child.Execute(blackboard);

                    switch (_currentState)
                    {
                        case BehaviorState.Failure:
                            continue;
                        case BehaviorState.Success:
                        case BehaviorState.Running:
                            return _currentState;
                    }
                }
                _currentState = BehaviorState.Failure;
                return _currentState;
            }
        }

        public class BehaviorSequence : BehaviorComposite
        {
            public BehaviorSequence(List<Behavior> childBehaviors) : base(childBehaviors)
            {
            }

            public override BehaviorState Execute(Blackboard blackboard)
            {
                foreach (var child in _childBehaviors)
                {
                    _currentState = child.Execute(blackboard);

                    switch (_currentState)
                    {
                        case BehaviorState.Running:
                        case BehaviorState.Failure:
                            return _currentState;
                        case BehaviorState.Success:
                            continue;
                    }
                }
                _currentState = BehaviorState.Success;
                return _currentState;
            }
        }
        #endregion

        #region CONDITIONAL
        public class BehaviorConditional : Behavior
        {
            private Func<Blackboard, bool> _evalFunc;

            public BehaviorConditional(Func<Blackboard, bool> evalFunc)
            {
                _evalFunc = evalFunc;
            }

            public override BehaviorState Execute(Blackboard blackboard)
            {
                if (_evalFunc == null)
                {
                    return BehaviorState.Failure;
                }
                return _evalFunc(blackboard) ? BehaviorState.Success : BehaviorState.Failure;
            }
        }

        public class BehaviorInvertConditional : Behavior
        {
            private Func<Blackboard, bool> _evalFunc;

            public BehaviorInvertConditional(Func<Blackboard, bool> evalFunc)
            {
                _evalFunc = evalFunc;
            }

            public override BehaviorState Execute(Blackboard blackboard)
            {
                if (_evalFunc == null)
                {
                    return BehaviorState.Failure;
                }
                return _evalFunc(blackboard) ? BehaviorState.Failure : BehaviorState.Success;
            }
        }
        #endregion

        public class BehaviorAction : Behavior
        {
            private Func<Blackboard, BehaviorState> _actionFunc;

            public BehaviorAction(Func<Blackboard, BehaviorState> actionFunc)
            {
                _actionFunc = actionFunc;
            }

            public override BehaviorState Execute(Blackboard blackboard)
            {
                if (_actionFunc == null)
                {
                    return BehaviorState.Failure;
                }
                _currentState = _actionFunc(blackboard);
                return _currentState;
            }
        }

        public interface IDecisionMaking
        {
            public void Update();
        }

        public class BehaviorTree : IDecisionMaking
        {
            public BehaviorState CurrentState { get; private set; } = BehaviorState.Failure;
            private Behavior _rootBehavior;
            public Blackboard Blackboard { get; private set; }

            public BehaviorTree(Blackboard blackboard, Behavior rootBehavior)
            {
                Blackboard = blackboard;
                _rootBehavior = rootBehavior;
            }

            public void Update()
            {
                if (_rootBehavior == null)
                {
                    CurrentState = BehaviorState.Failure;
                    return;
                }
                CurrentState = _rootBehavior.Execute(Blackboard);
            }
        }
    }
}