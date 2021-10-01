using System;

namespace DNDDateBase.Utility
{
    [Serializable]
    public abstract class DNDAppObj : IEquatable<DNDAppObj>
    {
        /// <summary>
        /// The objects unique ID. Used for comparison
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// The given name of the object. This could be a location name or a character name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// General notes
        /// </summary>
        public string Notes { get; set; }

        public DNDAppObj()
        {
            ID = Guid.NewGuid();
        }

        bool IEquatable<DNDAppObj>.Equals(DNDAppObj other)
        {
            return other.ID.Equals(ID);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
