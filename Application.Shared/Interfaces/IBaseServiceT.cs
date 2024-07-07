namespace Application.Shared.Interfaces;

public interface IBaseServiceT<TInput, TOut> where TInput : class
    where TOut : class
{
    Task<TOut> CreateAsync(TInput input, CancellationToken cancellationToken);
    //Task<TOut> UpdateAsync(Guid guidId, TInput input, CancellationToken cancellationToken);
    //Task<TOut> GetByIdAsync(Guid guidId, CancellationToken cancellationToken);
    //Task<TOut> DeleteByIdAsync(Guid guidId, CancellationToken cancellationToken);
}
