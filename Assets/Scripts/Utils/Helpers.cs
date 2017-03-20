using System;
using System.Collections;
using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Static helper methods
    /// </summary>
    public static class Helpers {

        /// <summary>
        /// Calculates the point of interception for a projectile fired from one object towards another
        /// </summary>
        /// <param name="sourcePosition">Position of projectile source</param>
        /// <param name="sourceVelocity">Velocity of projectile source</param>
        /// <param name="targetPosition">Position of projectile target</param>
        /// <param name="targetVelocity">Velocity of projectile target</param>
        /// <param name="projectileSpeed">Speed of projectile</param>
        /// <returns>Point of collision</returns>
        public static Vector3 InterceptionPoint(
            Vector3 sourcePosition,
            Vector3 sourceVelocity,
            Vector3 targetPosition,
            Vector3 targetVelocity,
            float projectileSpeed
        ) {
            Vector3 relativePosition = targetPosition - sourcePosition;
            Vector3 relativeVelocity = targetVelocity - sourceVelocity;
            float t = InterceptionPointTime(relativePosition, relativeVelocity, projectileSpeed);
            return targetPosition + t * relativeVelocity;
        }

        /// <summary>
        /// Calculates time until interception for projectile
        /// </summary>
        /// <param name="relativePosition">Relative position of source and target</param>
        /// <param name="relativeVelocity">Relative velocity of source and target</param>
        /// <param name="projectileSpeed">The speed of the projectile</param>
        /// <returns>Time until collision will occur</returns>
        private static float InterceptionPointTime(
            Vector3 relativePosition,
            Vector3 relativeVelocity,
            float projectileSpeed
        ) {
            float velocitySquared = relativeVelocity.sqrMagnitude;
            if (velocitySquared < 0.001f) {
                return 0f;
            }
            float a = velocitySquared - projectileSpeed * projectileSpeed;
            if (Mathf.Abs(a) < 0.001f) {
                float t = -relativePosition.sqrMagnitude / (2f * Vector3.Dot(relativeVelocity, relativePosition));
                return Mathf.Max(t, 0f);
            }
            float b = 2f * Vector3.Dot(relativeVelocity, relativePosition);
            float c = relativePosition.sqrMagnitude;
            float determinant = b * b - 4f * a * c;
            if (determinant > 0f) {
                float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a);
                float t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
                if (t1 > 0f) {
                    if (t2 > 0f) {
                        return Mathf.Min(t1, t2);
                    }
                    return t1;
                }
                return Mathf.Max(t2, 0f);
            }
            if (determinant < 0f) {
                return 0f;
            }
            return Mathf.Max(-b / (2f * a), 0f);
        }

        /// <summary>
        /// Starts a coroutine from gamecontroller that calls method after given seconds
        /// </summary>
        /// <param name="action">Method to call</param>
        /// <param name="time">Time to wait</param>
        /// <returns></returns>
        public static void CallAfter(Action action, float time) {
            GameController.Instance.StartCoroutine(CallAfterEnumerator(action, time));
        }

        /// <summary>
        /// Internal enumerator function for coroutine
        /// </summary>
        /// <param name="action">Method to call</param>
        /// <param name="time">Time to wait</param>
        /// <returns></returns>
        private static IEnumerator CallAfterEnumerator(Action action, float time) {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}
