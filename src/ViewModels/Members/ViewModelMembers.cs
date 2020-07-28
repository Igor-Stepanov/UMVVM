using System;
using System.Collections.Generic;
using System.Reflection;
using ViewModels.Commands;
using ViewModels.Types;

namespace ViewModels.Members
{
  public sealed class ViewModelMembers
  {
    private readonly IViewModel _instance;
    private readonly ViewModelType _type;

    private readonly Dictionary<string, IViewModelMember> _members;
    private readonly List<IViewModel> _viewModels;

    public ViewModelMembers(IViewModel instance, ViewModelType type)
    {
      _instance = instance;
      _type = type;

      _members = new Dictionary<string, IViewModelMember>(_type.MembersCount);
      _viewModels = new List<IViewModel>(_type.ViewModelsCount);
    }

    public void Initialize()
    {
      foreach (var field in _type.Fields)
      {
        var member = Member(field);
        _members.Add(field.Name, member);

        if (member is IViewModel viewModel)
        {
          _viewModels.Add(viewModel);
          viewModel.Initialize();
        }
      }

      foreach (var method in _type.Methods)
        _members.Add(method.Name, Member(method));
    }

    public void Enable()
    {
      foreach (var viewModel in _viewModels)
        viewModel.Enable();
    }

    public void Disable()
    {
      foreach (var viewModel in _viewModels)
        viewModel.Disable();
    }

    public void Terminate()
    {
      foreach (var viewModel in _viewModels)
        viewModel.Terminate();
        
      _members.Clear();
    }

    private IViewModelMember Member(FieldInfo field) =>
      (IViewModelMember) field.GetValue(_instance);

    private IViewModelMember Member(MethodInfo method)
    {
      var parameters = method.GetParameters();

      switch (parameters.Length)
      {
        case 0:
        {
          var action = (Action) Delegate.CreateDelegate(typeof(Action), _instance, method);
          return new Command(method.Name, action, _instance);
        }
        case 1:
        {
          var parameterType = parameters[0].ParameterType;

          var actionType = typeof(Action<>).MakeGenericType(parameterType);
          var commandType = typeof(Command<>).MakeGenericType(parameterType);

          var action = Delegate.CreateDelegate(actionType, _instance, method);
          return (IViewModelMember) Activator.CreateInstance(commandType, method.Name, action, _instance);
        }
        default:
          throw new NotSupportedException($"Method {_instance.GetType()}.{method.Name}" +
                                          $"has not supported parameters count.");
      }
    }

    public bool HasBy(string name, out IViewModelMember member) =>
      _members.TryGetValue(name, out member);
  }
}