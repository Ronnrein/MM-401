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
        /// The text element showing the level name
        /// </summary>
        public Text LevelText;

        /// <summary>
        /// The button to start
        /// </summary>
        public Transform LoadButton;

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
            _loader.allowSceneActivation = false;
            LevelText.text = "Chapter " + GameController.CurrentChapter.Id + ": Level " + GameController.CurrentLevel.Id;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update () {
            ProgressBar.value = _loader.progress;
            if (ProgressBar.gameObject.activeSelf && _loader.progress >= 0.9f) {
                ProgressBar.gameObject.SetActive(false);
                LoadButton.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Function to load the level
        /// </summary>
        public void Load() {
            _loader.allowSceneActivation = true;
        }

    }
}
