using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manifold.Level1
{
    public class Level1Puzzle1Button1Event1Duplicate : Duplicate
    {
        [SerializeField] Level1Puzzle1Button1Event1 main;

        void Start()
        {
            base.Start();
        }

        public override void Act1()
        {
            //Debug.Log("?");
            main.TriggerEvent1();
        }
    }
}
