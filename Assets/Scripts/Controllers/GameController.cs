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
        /// Static instance of this class
        /// </summary>
        public static GameController Instance { get; private set; }

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake() {

            // Create a static instance of this class and lock cursor
            Instance = this;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            LevelText.text = "Chapter " + CurrentChapter.Id + ": Level " + CurrentLevel.Id;
            MapPieceController.SpawnFirst();
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
