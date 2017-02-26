using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Static class containing extensions methods used in this project
    /// </summary>
    public static class Extensions {

        /// <summary>
        /// Destroys children objects of transform
        /// </summary>
        /// <param name="parent">Parent to empty</param>
        public static void DestroyChildren(this Transform parent) {
            foreach (Transform child in parent) {
                Object.Destroy(child.gameObject);
            }
        }
    }
}
