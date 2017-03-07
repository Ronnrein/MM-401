using UnityEngine;

namespace Assets.Scripts.Utils {

    /// <summary>
    /// Editor script for sequencing map pieces in the scene
    /// </summary>
    public class LevelSequencer : MonoBehaviour {

        /// <summary>
        /// The map pieces
        /// </summary>
        public Transform[] Pieces;

        /// <summary>
        /// Last places map piece
        /// </summary>
        private Transform _last;

        /// <summary>
        /// Sequence map pieces in the scene
        /// </summary>
        public void Sequence() {
            if (Pieces.Length <= 1) {
                return;
            }
            _last = Pieces[0];
            for(int i = 1; i < Pieces.Length; i++) {
                Transform piece = Pieces[i];
                piece.transform.position = new Vector3(
                    _last.position.x,
                    _last.position.y,
                    _last.position.z + _last.localScale.z / 2 + piece.transform.localScale.z / 2
                );
                _last = piece.transform;
            }
        }
    }
}
