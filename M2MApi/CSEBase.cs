using System;
using System.Collections.Generic;
using System.Linq;

namespace M2MApi
{
  public class CSEBase : ICSEBase
  {
    private readonly List<INode> nodes;

    public CSEBase()
    {
      nodes = new List<INode>();
      CreationTime = DateTimeOffset.UtcNow;
      LastModifiedTime = CreationTime;
    }

    public DateTimeOffset CreationTime { get; private set; }

    public DateTimeOffset LastModifiedTime { get; private set; }
    public IEnumerable<T> GetNodes<T>() where T : INode
    {
      return nodes.OfType<T>();
    }

    public T GetNode<T>(string id) where T : INode
    {
      return nodes.OfType<T>().FirstOrDefault(n => n.Id == id);
    }

    public void AddNode(INode node)
    {
      nodes.Add(node);
      LastModifiedTime = DateTimeOffset.UtcNow;
    }

    public void RemoveNode(INode node)
    {
      nodes.Remove(node);
      LastModifiedTime = DateTimeOffset.UtcNow;
    }
  }
}