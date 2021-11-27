using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Util
{
    public class TriggerEvent : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        
        // Update is called once per frame
        void Update()
        {
        
        }
    
        public UnityEvent CollisionEnterEvent;
        public UnityEvent CollisionExitEvent;
        public UnityEvent CollisionStayEvent;

        private void OnCollisionEnter(Collision other)
        {
            CollisionEnterEvent.Invoke();
        }

        private void OnCollisionExit(Collision other)
        {
            CollisionExitEvent.Invoke();
        }

        private void OnCollisionStay(Collision other)
        {
            CollisionStayEvent.Invoke();
        }

    }
}
