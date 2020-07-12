using System;
using System.Collections.Generic;
using System.Reflection;
using ViewModels.Commands;
using ViewModels.Types;

namespace ViewModels.Members
{
  public sealed class ViewModelMembers
  {
    private readonly IViewModel _viewModel;
    private readonly ViewModelType _type;

    private readonly Dictionary<string, IViewModelMember> _members;
    private readonly List<IViewModel> _viewModels;

    public ViewModelMembers(IViewModel viewModel, ViewModelType type)
    {
      _viewModel = viewModel;
      _type = type;

      _members = new Dictionary<string, IViewModelMember>(_type.MembersCount);
      _viewModels = new List<IViewModel>(_type.ViewModelsCount);
    }

    public void Enable()
    {
      Initialize();
      EnableViewModels();
    }

    public void Disable()
    {
      DisableViewModels();
      Clear();
    }

    private void Initialize()
    {
      foreach (var field in _type.Fields)
      {
        var member = Member(field);
        _members.Add(field.Name, member);
        
        if (member is IViewModel viewModel)
          _viewModels.Add(viewModel);
      }

      foreach (var method in _type.Methods)
        _members.Add(method.Name, Member(method));
    }

    private void EnableViewModels()
    {
      foreach (var viewModel in _viewModels)
        viewModel.Enable();
    }

    private void DisableViewModels()
    {
      foreach (var viewModel in _viewModels)
        viewModel.Disable();
    }

    private void Clear() =>
      _members.Clear();

    private IViewModelMember Member(FieldInfo field) =>
      (IViewModelMember) field.GetValue(_viewModel);

    private IViewModelMember Member(MethodInfo method)
    {
      var parameters = method.GetParameters();

      switch (parameters.Length)
      {
        case 0:
        {
          var action = (Action) Delegate.CreateDelegate(typeof(Action), _viewModel, method);
          return new Command(action);
        }
        case 1:
        {
          var parameterType = parameters[0].ParameterType;

          var actionType = typeof(Action<>).MakeGenericType(parameterType);
          var commandType = typeof(Command<>).MakeGenericType(parameterType);

          var action = Delegate.CreateDelegate(actionType, _viewModel, method);
          return (IViewModelMember) Activator.CreateInstance(commandType, action);
        }
        default:
          throw new NotSupportedException($"Method {_viewModel.GetType()}.{method.Name}" +
                                          $"has not supported parameters count.");
      }
    }

    public bool TryGetMember<T>(string name, out T member) where T : IViewModelMember
    {
      member = default(T);
      
      if (!_members.TryGetValue(name, out var value))
        return false;

      if (!(value is T castedMember))
        return false;
      
      member = castedMember;
      return true;
    }
  }
}