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
        /// The name of the level
        /// </summary>
        public string Name;

        /// <summary>
        /// The text that displays in the beginning
        /// </summary>
        public string Text;

        /// <summary>
        /// The path to the scene
        /// </summary>
        public string Scene;

        /// <summary>
        /// String representation of level
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return "Level " + Id;
        }

    }
}
