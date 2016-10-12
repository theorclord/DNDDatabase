using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DNDDateBase.Utility
{
  [Serializable]
  public abstract class DNDAppObj : IEquatable<DNDAppObj>
  {
    public string Name { get; set; }

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
