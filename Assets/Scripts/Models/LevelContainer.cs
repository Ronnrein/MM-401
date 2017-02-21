using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.Models {

    /// <summary>
    /// Container for all chapter and levels
    /// </summary>
    [XmlRoot("LevelCollection")]
    public class LevelContainer {

        /// <summary>
        /// Contains all chapters
        /// </summary>
        [XmlArray("Chapters")]
        [XmlArrayItem("Chapter")]
        public List<Chapter> Chapters = new List<Chapter>();

        /// <summary>
        /// Property creating and retrieving static instance of class
        /// </summary>
        public static LevelContainer Instance {
            get { return _instance ?? (_instance = Load()); }
        }

        /// <summary>
        /// Static instance of class
        /// </summary>
        private static LevelContainer _instance;

        /// <summary>
        /// Load the level hierarchy from xml file
        /// </summary>
        /// <returns>Fully populated object hierarchy of levels</returns>
        private static LevelContainer Load() {
            TextAsset xml = (TextAsset)Resources.Load("Levels");
            XmlSerializer serializer = new XmlSerializer(typeof(LevelContainer));
            return serializer.Deserialize(new StringReader(xml.text)) as LevelContainer;;
        }
	
    }
}
