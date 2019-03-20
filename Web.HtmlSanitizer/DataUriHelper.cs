using System;
using System.Text.RegularExpressions;

namespace Vereyon.Web {
  /// <summary>
  /// Helper for data URI
  /// </summary>
  public class DataUriHelper {
    /// <summary>
    /// Regular expression to extract data URI elements
    /// </summary>
    private const string DataUriFormat = @"(?<scheme>[\w]+):(?<mime>[\w/\-\.\+]+);(?<encoding>\w+),(?<data>.*)";

    /// <summary>
    /// </summary>
    private static readonly Regex DataUriRegex = new Regex (DataUriFormat, RegexOptions.Compiled);

    /// <summary>
    /// Parse data URI content to extract element from it.
    /// </summary>
    public static DataUriDef Parse (string content) {
      var match = DataUriRegex.Match (content);
      if (!match.Success || match.Groups.Count < 4) {
        return null;
      }
      if (!string.Equals ("base64", match.Groups["encoding"].Value, StringComparison.OrdinalIgnoreCase)) {
        return null;
      }
      return new DataUriDef {
        Scheme = match.Groups["scheme"].Value,
          Mime = match.Groups["mime"].Value,
          Encoding = match.Groups["encoding"].Value,
          Data = match.Groups["data"].Value
      };
    }

    /// <summary>
    /// Definition of data URI
    /// </summary>
    public class DataUriDef {
      /// <summary>
      /// Uri scheme
      /// </summary>
      public string Scheme { get; set; }
      /// <summary>
      /// Mime type
      /// </summary>
      public string Mime { get; set; }

      /// <summary>
      /// Encoding
      /// </summary>
      public string Encoding { get; set; }

      /// <summary>
      /// base64 data
      /// </summary>
      public string Data { get; set; }
    }
  }
}