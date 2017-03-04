using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Models;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers {

    /// <summary>
    /// Controls the main menu
    /// </summary>
    public class MainMenuController : MonoBehaviour {

        /// <summary>
        /// Vertical margin between buttons
        /// </summary>
        public float ButtonMargin = 40f;

        /// <summary>
        /// Time to complete UI animations
        /// </summary>
        public float AnimationTime = 0.5f;

        /// <summary>
        /// Container for chapter buttons
        /// </summary>
        public Transform ChapterBtnContainer;

        /// <summary>
        /// Container for level buttons
        /// </summary>
        public Transform LevelBtnContainer;

        /// <summary>
        /// Container for main buttons
        /// </summary>
        public Transform MainBtnContainer;

        /// <summary>
        /// Prefab of a button
        /// </summary>
        public GameObject ButtonPrefab;

        /// <summary>
        /// Currently selected chapter in UI
        /// </summary>
        private Chapter _selectedChapter;

        /// <summary>
        /// Fires when game is started, after Awake
        /// </summary>
        public void Start() {
		    CreateMenuButtons();
        }

        /// <summary>
        /// Toggles whether chapter buttons are shown or not
        /// </summary>
        public void ToggleChapterButtons() {
            if (ChapterBtnContainer.childCount != 0) {
                ChapterBtnContainer.DestroyChildren();
                LevelBtnContainer.DestroyChildren();
                return;
            }
            List<Chapter> chapters = LevelContainer.Instance.Chapters;
            for(int i = 0; i < chapters.Count; i++) {
                Chapter chapter = chapters[i];
                Vector3 position = new Vector3(0, -(ButtonMargin + ButtonPrefab.transform.localScale.y) * i, 0.1f * i);
                GameObject button = CreateButton(chapter.ToString(), Vector3.zero, ChapterBtnContainer);
                LerpTowards.LerpObject(button, ChapterBtnContainer.TransformPoint(position), AnimationTime, true);
                button.GetComponent<Button>().onClick.AddListener(delegate {
                    ToggleLevelButtons(chapter);
                });
            }
        }

        /// <summary>
        /// Toggles whether level buttons for current chapter are shown
        /// </summary>
        /// <param name="chapter">The chapter to show buttons for</param>
        private void ToggleLevelButtons(Chapter chapter) {
            LevelBtnContainer.DestroyChildren();
            if (chapter == _selectedChapter) {
                return;
            }
            _selectedChapter = chapter;
            List<Level> levels = chapter.Levels;
            for (int i = 0; i < levels.Count; i++) {
                Level level = levels[i];
                Vector3 position = new Vector3(0, -(ButtonMargin + ButtonPrefab.transform.localScale.y) * i, 0.1f * i);
                GameObject button = CreateButton(level.ToString(), Vector3.zero, LevelBtnContainer);
                LerpTowards.LerpObject(button, LevelBtnContainer.TransformPoint(position), AnimationTime, true);
                button.GetComponent<Button>().onClick.AddListener(delegate {
                    GameController.LoadLevel(_selectedChapter, level);
                });
            }
        }

        /// <summary>
        /// Creates the basic menu buttons
        /// </summary>
        private void CreateMenuButtons() {
            float offset = ButtonMargin + ButtonPrefab.transform.localScale.y;
            GameObject newGameBtn = CreateButton("New game", Vector3.zero, MainBtnContainer);
            newGameBtn.GetComponent<Button>().onClick.AddListener(delegate {
                Chapter first = LevelContainer.Instance.Chapters.First();
                GameController.LoadLevel(first, first.Levels.First());
            });
            GameObject levelSelectBtn = CreateButton("Level select", new Vector3(0, -offset, 0), MainBtnContainer);
            levelSelectBtn.GetComponent<Button>().onClick.AddListener(ToggleChapterButtons);
            GameObject quitBtn = CreateButton("Quit", new Vector3(0, -offset * 2, 0), MainBtnContainer);
            quitBtn.GetComponent<Button>().onClick.AddListener(Application.Quit);
        }

        /// <summary>
        /// Creates a button in the UI
        /// </summary>
        /// <param name="text">Text content of the button</param>
        /// <param name="position">Local position of the button</param>
        /// <param name="parent">Parent transform of the button</param>
        /// <returns>Created button object</returns>
        private GameObject CreateButton(string text, Vector3 position, Transform parent = null) {
            GameObject button = Instantiate(ButtonPrefab);
            button.transform.SetParent(parent);
            button.transform.localPosition = position;
            button.transform.GetChild(0).GetComponent<Text>().text = text;
            return button;
        }

    }
}
