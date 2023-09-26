using System.Collections.Generic;
using UnityEngine;

namespace that
{
    namespace BT
    {
        public interface IBlackboardField
        {
        }

        public class BlackboardField<T> : IBlackboardField
        {
            public BlackboardField(T data)
            {
                Data = data;
            }
            public T Data { get; set; }
        }

        public class Blackboard
        {
            private Dictionary<string, IBlackboardField> _blackBoardData;

            public bool AddData<T>(string name, T data)
            {
                if (_blackBoardData.ContainsKey(name))
                {
                    Debug.LogWarning($"{name} is already present in the blackboard!");
                    return false;
                }
                _blackBoardData.Add(name, new BlackboardField<T>(data));
                return true;
            }

            public bool ChangeData<T>(string name, T data)
            {
                if (_blackBoardData.ContainsKey(name))
                {
                    BlackboardField<T> bbf = (BlackboardField<T>)_blackBoardData[name];
                    if (bbf != null)
                    {
                        bbf.Data = data;
                        return true;
                    }
                }
                Debug.LogWarning($"{name} is not present in the blackboard!");
                return false;
            }

            public bool GetData<T>(string name, out T data)
            {
                BlackboardField<T> bbf = (BlackboardField<T>)_blackBoardData[name];
                if (bbf != null)
                {
                    data = bbf.Data;
                    return true;
                }
                Debug.LogWarning($"{name} is not present in the blackboard!");
                data = default;
                return false;
            }
        }
    }
}