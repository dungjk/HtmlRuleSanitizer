using HtmlAgilityPack;

namespace Vereyon.Web {
  /// <summary></summary>
  public enum SanitizerOperation {
    /// <summary>
    /// Default operation. Does nothing.
    /// </summary>
    DoNothing = 0,

    /// <summary>
    /// Strip the tag while preserving it's contents.
    /// </summary>
    FlattenTag,

    /// <summary>
    /// Completely remove the tag and it's contents.
    /// </summary>
    RemoveTag,

    /// <summary>
    /// Removes only the attribute while preserving the tag itself.
    /// </summary>
    RemoveAttribute
  }
}