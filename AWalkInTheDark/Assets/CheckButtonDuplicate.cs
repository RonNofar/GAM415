using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold
{
    public class CheckButtonDuplicate : Duplicate
    {
        public CheckButton checkButton;
        public SpriteRenderer SR;

        void Start()
        {
            base.Start();
        }

        public override void Act1()
        {
            SR.sprite = checkButton.checkOV[1];
        }
    }
}
