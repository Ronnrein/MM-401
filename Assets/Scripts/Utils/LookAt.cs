﻿using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Forces object to look at another object
    /// </summary>
    public class LookAt : MonoBehaviour {

        /// <summary>
        /// Target to look at
        /// </summary>
        public Transform Target;

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            transform.LookAt(Target);
        }
    }
}
