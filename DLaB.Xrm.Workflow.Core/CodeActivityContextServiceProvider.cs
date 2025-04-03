using System;
using System.Activities;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM_WORKFLOW
namespace DLaB.Xrm.Workflow
#else
namespace Source.DLaB.Xrm.Workflow
#endif
{
    public class CodeActivityContextServiceProvider : IServiceProvider
    {
        private readonly CodeActivityContext _context;

        public CodeActivityContextServiceProvider(CodeActivityContext context)
        {
            _context = context;
        }

        public object? GetService(Type serviceType)
        {
            return typeof(CodeActivityContext) == serviceType
                ? _context
                : typeof(CodeActivityContext)
                    .GetMethod("GetExtension")
                    ?.MakeGenericMethod(serviceType)
                    .Invoke(_context, null);
        }
    }
}
