using Models;
using Models.Acquisition;
using Models.Source;

namespace ViewModels
{
  public abstract class RefreshableViewModel<TModel> : ViewModel
    where TModel : IModel
  {
    private readonly ISource<TModel> _source;

    protected RefreshableViewModel(ISource<TModel> source) => 
      _source = source;

    public void RefreshWith(IAcquired<TModel> acquired) =>
      RefreshWith(acquired.Model);

    protected override void Enable()
    {
      using (var acquired =_source.AcquireModel())
        RefreshWith(acquired.Model);
    }

    protected abstract void RefreshWith(TModel model);
  }
}