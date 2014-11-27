using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace M2MApi
{
  public interface ICSEBase
  {
    DateTimeOffset CreationTime { get; }
    DateTimeOffset LastModifiedTime { get; }

    IEnumerable<T> GetNodes<T>() where T: INode;
    T GetNode<T>(string id) where T : INode;
    void AddNode(INode node);
    void RemoveNode(INode node);
  }
}
