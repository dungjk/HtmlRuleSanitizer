using System;
using System.Text.RegularExpressions;

namespace Vereyon.Web {
  public class DataUriHelper {
    private const string DataUriFormat = @"(?<scheme>[\w]+):(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)";
    private static readonly Regex DataUriRegex = new Regex (DataUriFormat, RegexOptions.Compiled);

    public static DataUriDef Parse (string content) {
      var match = DataUriRegex.Match (content);
      if (!match.Success || match.Groups.Count != 4) {
        return null;
      }
      if (!string.Equals ("base64", match.Groups["encoding"].Value, StringComparison.OrdinalIgnoreCase)) {
        return null;
      }
      return new DataUriDef {
        Scheme = match.Groups["scheme"].Value,
          Mime = match.Groups["mime"].Value,
          Data = match.Groups["data"].Value
      };
    }

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
      /// base64 data
      /// </summary>
      public string Data { get; set; }
    }
  }
}