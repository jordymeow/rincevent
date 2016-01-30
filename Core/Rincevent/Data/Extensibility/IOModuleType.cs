namespace Meow.FR.Rincevent.Core.Extensibility
{
    /// <summary>
    /// Type of IO module. It can be an import module, export module, or can be able to manage both.
    /// </summary>
    public enum IOModuleType
    {
        /// <summary>
        /// Manages export and import.
        /// </summary>
        Both,
        /// <summary>
        /// Manages only import.
        /// </summary>
        Import,
        /// <summary>
        /// Manages only export.
        /// </summary>
        Export
    }
}
