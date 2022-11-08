namespace DLaB.Xrm.Plugin
{
    /// <summary>
    /// Defines the type of Context Entity
    /// </summary>
    public enum ContextEntity
    {
        /// <summary>
        /// The result of the target coalesced with the post image
        /// </summary>
        CoalesceTargetPostImage,
        /// <summary>
        /// The result of the target coalesced with the pre image
        /// </summary>
        CoalesceTargetPreImage,
        /// <summary>
        /// The post image
        /// </summary>
        PostImage,
        /// <summary>
        /// The pre image
        /// </summary>
        PreImage,
        /// <summary>
        /// The target
        /// </summary>
        Target,
    }
}
