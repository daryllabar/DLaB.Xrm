using Source.DLaB.Xrm.Ioc;

#if DLAB_UNROOT_NAMESPACE || DLAB_XRM
namespace DLaB.Xrm.Plugin
#else
namespace Source.DLaB.Xrm.Plugin
#endif

{
    /// <summary>
    /// Plugin Interface for the extended plugin
    /// </summary>
    public interface IExtendedPlugin : IRegisteredEventsPlugin, IContainerWrapper
    {
    }
}
