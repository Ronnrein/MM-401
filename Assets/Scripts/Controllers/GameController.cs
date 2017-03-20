using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Game controller that persists through levels
    /// </summary>
    public class GameController : MonoBehaviour {

        /// <summary>
        /// Lives of player
        /// </summary>
        public int StartingLives = 3;

        /// <summary>
        /// The ship controller
        /// </summary>
        public ShipController Player;

        /// <summary>
        /// The movement controller
        /// </summary>
        public MovementController Movement;

        /// <summary>
        /// Level text object
        /// </summary>
        public TextMesh LevelText;

        /// <summary>
        /// Gameobject of pause menu
        /// </summary>
        public GameObject PauseMenu;

        /// <summary>
        /// Text representing player lives
        /// </summary>
        public Text LivesText;

        /// <summary>
        /// Lives property
        /// </summary>
        public int Lives {
            get { return _lives; }
            set {
                _lives = value;
                LivesText.text = "Lives: " + _lives;
            }
        }

        /// <summary>
        /// Current level
        /// </summary>
        public static Level CurrentLevel;

        /// <summary>
        /// Current chapter
        /// </summary>
        public static Chapter CurrentChapter;

        /// <summary>
        /// Static instance of this class
        /// </summary>
        public static GameController Instance { get; private set; }

        /// <summary>
        /// The z value the ship has to reach before the game ends
        /// </summary>
        private float _endGameZPosition = float.MaxValue;

        private int _lives;

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
            LevelText.text = CurrentLevel.Name;

            // Try to get the position of the end of the level
            LevelSequencer sequencer = GetComponent<LevelSequencer>();
            if (sequencer != null) {
                Transform last = sequencer.Pieces.Last();
                _endGameZPosition = last.position.z - last.localScale.z / 2;
            }

            _lives = StartingLives;
            Lives = _lives;
        }

        /// <summary>
        /// Fires when game updates
        /// </summary>
        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                TogglePause();
            }
            if (Player.transform.position.z > _endGameZPosition) {
                Player.PlayEndingAnimation();
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
        /// Loads main menu, non static version for UI
        /// </summary>
        public void UiLoadMainMenu() {
            LoadMainMenu();
        }

        /// <summary>
        /// Loads main menu
        /// </summary>
        public static void LoadMainMenu() {
            Time.timeScale = 1;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
            SceneManager.LoadScene("Scenes/Levels/" + level.Scene);
        }

        /// <summary>
        /// Ends level
        /// </summary>
        public static void EndLevel() {
            LoadMainMenu();
        }

    }
}
