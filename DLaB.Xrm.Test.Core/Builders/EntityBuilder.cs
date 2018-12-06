using Microsoft.Xrm.Sdk;

namespace DLaB.Xrm.Test.Core.Builders
{
    public abstract class EntityBuilder<TEntity> : DLaB.Xrm.Test.Builders.EntityBuilder<TEntity> where TEntity : Entity
    {

    }
}
