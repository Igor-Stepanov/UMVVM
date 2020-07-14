using System;
using UnityEngine;

namespace Views.Bindings
{
  public abstract class Binding : MonoBehaviour, IBinding
  {
    public void Bind()
    {
      throw new System.NotImplementedException();
    }

    public void Unbind()
    {
      throw new System.NotImplementedException();
    }
  }
}