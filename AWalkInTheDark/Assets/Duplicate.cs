using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manifold
{
    [ExecuteInEditMode]
    public class Duplicate : MonoBehaviour
    {
        [SerializeField] DuplicateHandler dupHand;
        [HideInInspector]
        public Dictionary<int, UnityAction> acts = new Dictionary<int, UnityAction>();

        public bool debugCheck = false;

        public void Start()
        {
            dupHand.dups.Add(this);
            //Debug.Log("Addidng:" + gameObject.name);

            InitializeActs();
        }

        public void Action(UnityAction act)
        {
            act.Invoke();
            //Debug.Log("Action Invoked:" + gameObject.transform.parent);
        }

        #region Act Scripts

        private void InitializeActs()
        {
            SetAct(1, Act1);
        }

        private void SetAct(int num, UnityAction act)
        {
            acts[num] = act;
        }

        virtual public void Act1()
        {
            Debug.Log("NO");
        }

#endregion
    }
}
