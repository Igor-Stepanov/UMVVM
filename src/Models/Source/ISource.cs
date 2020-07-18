using Models.Acquisition;

namespace Models.Source
{
  public interface ISource<out TModel> where TModel : IModel
  {
    IAcquired<TModel> AcquireModel();
  }
}