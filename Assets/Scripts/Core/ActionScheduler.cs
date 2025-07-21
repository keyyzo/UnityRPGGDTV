using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {

        IAction previousAction;

        public void StartAction(IAction action)
        {
            
            if (previousAction == action)
                return;

            if (previousAction != null)
            {
                Debug.Log("Cancelling " + previousAction);
                previousAction.Cancel();
            }
            previousAction = action;
        }

        public void CancelCurrentAction()
        { 
            StartAction(null);
        }
        
    }
}
