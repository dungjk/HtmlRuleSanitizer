using HtmlAgilityPack;

namespace Vereyon.Web {
  /// <summary>
  /// An action on a Htmlnode
  /// </summary>
  public delegate void NodeSanitizerOperationDelegate (HtmlNode node);
  public delegate void AttributeSanitizerOperationDelegate (HtmlAttribute attribute);

  /// <summary>
  /// Predefined action on a HtmlNode
  /// </summary>
  public static class NodeSanitizerOperations {
    /// <summary>
    /// Do nothing with current node
    /// </summary>
    public static readonly NodeSanitizerOperationDelegate DoNothing = (n) => { };

    /// <summary>
    /// Remove current node
    /// </summary>
    public static readonly NodeSanitizerOperationDelegate RemoveTag = (n) => { n.Remove (); };

    /// <summary>
    /// Do nothing with current node
    /// </summary>
    public static readonly NodeSanitizerOperationDelegate FlatternTag = (n) => { };
  }

  public static class AttributeSanertizerOperations {
    /// <summary>
    /// Remove current attribute
    /// </summary>
    public static readonly AttributeSanitizerOperationDelegate RemoveAttribute = (a) => { a.Remove (); };
  }

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