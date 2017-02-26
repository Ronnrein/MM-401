using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Models {

    /// <summary>
    /// A level of the game
    /// </summary>
    public class Level {

        /// <summary>
        /// The id of the level
        /// </summary>
        [XmlAttribute("id")]
        public int Id;

        /// <summary>
        /// The text that displays in the beginning
        /// </summary>
        public string Text;

        /// <summary>
        /// The path to the piece pool
        /// </summary>
        public string PiecePath;

        /// <summary>
        /// The name of the start piece
        /// </summary>
        public string StartPiece;

        /// <summary>
        /// The name of the end piece
        /// </summary>
        public string EndPiece;

        /// <summary>
        /// The prefab to appear at the start of the level
        /// </summary>
        public GameObject StartPrefab {
            get { return GetPrefab(StartPiece); }
        }

        /// <summary>
        /// Array of special pieces to use
        /// </summary>
        [XmlArray("SpecialPieces")]
        [XmlArrayItem("SpecialPiece")]
        public List<string> SpecialPieces = new List<string>();

        /// <summary>
        /// Get a random prefab from the piece pool
        /// </summary>
        /// <returns>A random prefab to create</returns>
        public GameObject GetRandomPrefab() {
            Object[] objects = Resources.LoadAll("Levels/" + PiecePath);
            GameObject[] prefabs = Array.ConvertAll(objects, x => (GameObject)x);
            prefabs = prefabs.Where(x => x.name[0] != '_').ToArray();
            return prefabs[new System.Random().Next(prefabs.Length)];
        }

        /// <summary>
        /// String representation of level
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return "Level " + Id;
        }

        /// <summary>
        /// Get a prefab of this level with the specified name
        /// </summary>
        /// <param name="name">Name of the prefab</param>
        /// <returns>Prefab to use</returns>
        private GameObject GetPrefab(string name) {
            return Resources.Load("Levels/" + PiecePath + "/" + name) as GameObject;
        }

    }
}
