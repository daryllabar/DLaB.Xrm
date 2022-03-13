using Microsoft.Xrm.Sdk;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm
#else
namespace Source.DLaB.Xrm
#endif
{
    public static partial class Extensions
    {
        /// <summary>
        /// Casts the response
        /// </summary>
        /// <typeparam name="TDerived">Derived Type</typeparam>
        /// <param name="task">the Task</param>
        public static Task<TDerived> CastResponse<TDerived>(this Task<OrganizationResponse> task) where TDerived:OrganizationResponse
        {
            return task.CastType<TDerived, OrganizationResponse>();
        }

        /// <summary>
        /// Casts the response
        /// </summary>
        /// <typeparam name="TDerived">Derived Type</typeparam>
        /// <typeparam name="TBase">The Base Type</typeparam>
        /// <param name="task">the Task</param>
        /// <returns></returns>
        public static Task<TDerived> CastType<TDerived, TBase>(this Task<TBase> task) where TDerived:TBase
        {
            var res = new TaskCompletionSource<TDerived>();

            return task.ContinueWith(t =>
                {
                    if (t.IsCanceled)
                    {
                        res.TrySetCanceled();
                    }
                    else if (t.IsFaulted)
                    {
                        res.TrySetException(t.Exception ?? new AggregateException("Task was Faulted but contained no exception!"));
                    }
                    else
                    {
                        res.TrySetResult((TDerived)t.Result);
                    }
                    return res.Task;
                }
                , TaskContinuationOptions.ExecuteSynchronously).Unwrap();
        }
    }
}
