using System;
using UnityEngine;

namespace UIPages
{
    public abstract class UIPage : MonoBehaviour
    {
    
        public event Action PageActivatedEvent = ()=>{};
        public UIPage previousPage;
        public string previousScene;

        public void ActivatePage()
        {
            PageActivatedEvent.Invoke();
            OnPageActivating();
        }

        public void DisablePage()
        {
            OnPageClosing();
        }

        protected virtual void OnPageActivating(){}
        protected virtual void OnPageClosing(){}
    }
}
