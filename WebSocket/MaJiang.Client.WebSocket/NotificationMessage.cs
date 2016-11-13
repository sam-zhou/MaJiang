using System;

namespace MaJiang.Client.WebSocket
{
  internal class NotificationMessage
  {
    public string Body {
      get; set;
    }

    public string Icon {
      get; set;
    }

    public string Summary {
      get; set;
    }

    public override string ToString ()
    {
      return String.Format ("{0}: {1}", Summary, Body);
    }
  }
}
