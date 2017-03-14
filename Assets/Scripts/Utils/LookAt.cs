using System;
using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Forces object to look at another object
    /// </summary>
    public class LookAt : MonoBehaviour {

        /// <summary>
        /// Enum of directions to lock
        /// </summary>
        public enum LockDirection {
            None,
            X,
            Y
        };

        /// <summary>
        /// Target to look at
        /// </summary>
        public Transform Target;

        /// <summary>
        /// Speed of rotation, value from 0 to 1
        /// </summary>
        public float Speed = 1f;

        /// <summary>
        /// Reverse direction
        /// </summary>
        public bool Reverse = false;

        /// <summary>
        /// Direction to lock in
        /// </summary>
        public LockDirection Lock;

        /// <summary>
        /// Transform to determine where is up, default is attached object
        /// </summary>
        public Transform WorldSpace;

        /// <summary>
        /// Fires when game starts
        /// </summary>
        public void Awake() {
            if (WorldSpace == null) {
                WorldSpace = transform;
            }
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            if (Target != null) {
                Quaternion oldRotation = transform.localRotation;
                Vector3 lookDirection = (Target.position - transform.position).normalized;
                Vector3 localDirection = WorldSpace.InverseTransformDirection(lookDirection);
                if (Lock == LockDirection.X) {
                    localDirection.y = 0;
                }
                else if (Lock == LockDirection.Y) {
                    localDirection.x = 0;
                }
                lookDirection = WorldSpace.TransformDirection(localDirection);
                Quaternion lookRotation = Quaternion.LookRotation(Reverse ? -lookDirection : lookDirection, WorldSpace.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Speed);
                transform.localRotation = Quaternion.Euler(
                    Lock == LockDirection.X ? oldRotation.eulerAngles.x : transform.localRotation.eulerAngles.x,
                    Lock == LockDirection.Y ? oldRotation.eulerAngles.y : transform.localRotation.eulerAngles.y,
                    oldRotation.eulerAngles.z
                );
            }
        }
    }
}
