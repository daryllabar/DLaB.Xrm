using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test.Core.Builders
{
    public abstract class EntityBuilder<TEntity> : DLaB.Xrm.Test.Builders.DLaBEntityBuilder<TEntity, EntityBuilder<TEntity>> where TEntity : Entity
    {

    }
}
