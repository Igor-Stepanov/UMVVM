namespace Models.Acquisition
{
  public interface IAcquire<out TModel> where TModel : IModel
  {
    TModel Model { get; }
  }
}