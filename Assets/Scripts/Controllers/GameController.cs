using System.Linq;
using Assets.Scripts.Models;
using UnityEngine;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Game controller that persists through levels
    /// </summary>
    public class GameController : MonoBehaviour {

        /// <summary>
        /// The ship controller
        /// </summary>
        [HideInInspector]
        public ShipController Player;

        /// <summary>
        /// The movement controller
        /// </summary>
        [HideInInspector]
        public MovementController Movement;

        /// <summary>
        /// Current level
        /// </summary>
        [HideInInspector]
        public Level CurrentLevel;

        /// <summary>
        /// Current chapter
        /// </summary>
        [HideInInspector]
        public Chapter CurrentChapter;

        /// <summary>
        /// Static instance of this class
        /// </summary>
        public static GameController Instance { get; private set; }

        /// <summary>
        /// Fires when game is started
        /// </summary>
        public void Awake() {

            // Create a static instance of this class and lock cursor
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this) {

                // If singleton already exists, destroy this to enforce singleton pattern
                Destroy(gameObject);
            }
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            CurrentChapter = LevelContainer.Instance.Chapters.Single(x => x.Id == 1);
            CurrentLevel = CurrentChapter.Levels.Single(x => x.Id == 1);
            MapPieceController.SpawnFirst();
        }

    }
}
