using System;
using UnityEngine;

namespace Views.Bindings
{
  public abstract class Binding : MonoBehaviour, IBinding
  {
    protected View View;

    private void Awake()
    {
      View = NearestView();
    }

    public void Bind()
    {
      
    }

    public void Unbind() => 
      throw new NotImplementedException();

    private View NearestView() => 
      throw new NotImplementedException();
  }
}