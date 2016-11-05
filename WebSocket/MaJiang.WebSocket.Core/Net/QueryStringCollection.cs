using System;
using System.Collections.Specialized;
using System.Text;

namespace MaJiang.WebSocket.Core.Net
{
  internal sealed class QueryStringCollection : NameValueCollection
  {
    public override string ToString ()
    {
      var cnt = Count;
      if (cnt == 0)
        return String.Empty;

      var output = new StringBuilder ();
      var keys = AllKeys;
      foreach (var key in keys)
        output.AppendFormat ("{0}={1}&", key, this [key]);

      if (output.Length > 0)
        output.Length--;

      return output.ToString ();
    }
  }
}
