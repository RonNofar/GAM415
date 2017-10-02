using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Manifold
{
    [ExecuteInEditMode]
    public class DuplicateHandler : MonoBehaviour
    {
        public List<Duplicate> dups;

        private void Awake()
        {
            dups.Clear();
        }

        public void InvokeActionInDuplicates(int actNum)
        {
            foreach(Duplicate d in dups)
            {
                //d.Action(act);
                EnableActOnDuplicate(d, actNum);

                d.debugCheck = true;
            }
        }

        public void EnableActOnDuplicate(Duplicate dup, int actNum)
        {
            dup.acts[actNum].Invoke();
        }
    }
}
