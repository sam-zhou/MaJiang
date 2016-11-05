namespace MaJiang.WebSocket.Core.Net
{
  internal enum InputChunkState
  {
    None,
    Data,
    DataEnded,
    Trailer,
    End
  }
}
