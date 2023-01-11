#if NET
using DataverseUnitTest.Builders;
#else
using DLaB.Xrm.Test.Builders;
#endif
using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test.Core.Builders
{
    public abstract class EntityBuilder<TEntity> : DLaBEntityBuilder<TEntity, EntityBuilder<TEntity>> where TEntity : Entity
    {

    }
}
