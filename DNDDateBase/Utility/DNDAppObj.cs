using System;

namespace DNDDateBase.Utility
{
    [Serializable]
    public abstract class DNDAppObj : IEquatable<DNDAppObj>
    {
        /// <summary>
        /// The given name of the object. This could be a location name or a character name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// General notes
        /// </summary>
        public string Notes { get; set; }

        bool IEquatable<DNDAppObj>.Equals(DNDAppObj other)
        {
            return other.Name.Equals(Name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
