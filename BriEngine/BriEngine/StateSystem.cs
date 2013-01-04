using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class StateSystem
    {
        Dictionary<string, IGameObject> stateStore = new Dictionary<string, IGameObject>();
        IGameObject currentState = null;

        public void Update(double elapsedTime)
        {
            if (currentState == null)
                return;
            currentState.Update(elapsedTime);
        }

        public void Render()
        {
            if (currentState == null)
                return;
            currentState.Render();
        }

        public void AddState(string stateId, IGameObject state)
        {
            stateStore.Add(stateId, state);
        }

        public void ChangeState(string stateId)
        {
           //System.Diagnostics.Debug.Assert(Exists(stateId) == false);
            currentState = stateStore[stateId];
        }

        public bool Exists(string stateId)
        {
            //System.Diagnostics.Debug.Assert(Exists(stateId));
            return stateStore.ContainsKey(stateId);
        }
    }
}
