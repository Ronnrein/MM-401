using System.Linq;
using Assets.Scripts.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Game controller that persists through levels
    /// </summary>
    public class GameController : MonoBehaviour {

        /// <summary>
        /// The ship controller
        /// </summary>
        public ShipController Player;

        /// <summary>
        /// The movement controller
        /// </summary>
        public MovementController Movement;

        /// <summary>
        /// Current level
        /// </summary>
        public static Level CurrentLevel;

        /// <summary>
        /// Current chapter
        /// </summary>
        public static Chapter CurrentChapter;

        /// <summary>
        /// Level text object
        /// </summary>
        public TextMesh LevelText;

        /// <summary>
        /// Gameobject of pause menu
        /// </summary>
        public GameObject PauseMenu;

        /// <summary>
        /// Static instance of this class
        /// </summary>
        public static GameController Instance { get; private set; }

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake() {

            // Create a static instance of this class
            Instance = this;

            // Start first level if none is selected
            if (CurrentChapter == null) {
                CurrentChapter = LevelContainer.Instance.Chapters.First();
                CurrentLevel = CurrentChapter.Levels.First();
            }

            // Set up scene
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            LevelText.text = "Chapter " + CurrentChapter.Id + ": Level " + CurrentLevel.Id;
            MapPieceController.SpawnFirst();
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
        }

        /// <summary>
        /// Toggles the pause state and menu
        /// </summary>
        public void TogglePause() {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            Time.timeScale = 1 - Time.timeScale;
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.visible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        /// <summary>
        /// Loads main menu
        /// </summary>
        public void LoadMainMenu() {
            Time.timeScale = 1;
            SceneManager.LoadScene("Scenes/Menu");
        }

        /// <summary>
        /// Load and start a level
        /// </summary>
        /// <param name="chapter">Chapter to load</param>
        /// <param name="level">Level to load</param>
        public static void LoadLevel(Chapter chapter, Level level) {
            CurrentChapter = chapter;
            CurrentLevel = level;
            SceneManager.LoadScene("Scenes/Main");
        }

    }
}
