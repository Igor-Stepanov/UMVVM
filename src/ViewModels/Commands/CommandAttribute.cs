using System;

namespace ViewModels.Commands
{
  public class CommandAttribute : Attribute
  {
    public string Name { get; }

    public CommandAttribute(string name) =>
      Name = name;
  }
}