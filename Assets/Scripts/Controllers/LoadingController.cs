using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controller for loading screen
    /// </summary>
    public class LoadingController : MonoBehaviour {

        /// <summary>
        /// The progress bar
        /// </summary>
        public Slider ProgressBar;

        /// <summary>
        /// The loader async operation
        /// </summary>
        private AsyncOperation _loader;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start () {
            _loader =
                SceneManager.LoadSceneAsync(
                    "Scenes/Levels/" + GameController.CurrentLevel.Scene,
                    LoadSceneMode.Single
                );
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            ProgressBar.value = _loader.progress;
        }

    }
}
