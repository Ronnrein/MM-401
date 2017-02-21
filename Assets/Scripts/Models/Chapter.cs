using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.Scripts.Models {

    /// <summary>
    /// A chapter of levels
    /// </summary>
    public class Chapter {

        /// <summary>
        /// The id of the chapter
        /// </summary>
        [XmlAttribute("id")]
        public int Id;

        /// <summary>
        /// The levels of the chapter
        /// </summary>
        [XmlArray("Levels")]
        [XmlArrayItem("Level")]
        public List<Level> Levels;

    }
}
