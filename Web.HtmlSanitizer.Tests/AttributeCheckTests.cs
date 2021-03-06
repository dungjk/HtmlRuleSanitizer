﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Vereyon.Web {
  public class AttributeCheckTests {

    /// <summary>
    /// Tests if obviously illegal URL's are caught while obviously legal ones are left alone.
    /// </summary>
    [Fact]
    public void AHrefUrlCheckTest () {

      string result;
      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("a").CheckAttribute ("href", HtmlSanitizerCheckType.Url);

      // Test some illegal href
      var inputIllegal = @"<a href=""javascript:alert('test')"">That XSS trick</a>";
      var expectedIllegal = @"That XSS trick";
      result = sanitizer.Sanitize (inputIllegal);
      Assert.Equal (expectedIllegal, result);

      // Test a legal well formed url
      var inputLegal = @"<a href=""http://www.google.com/"">Legal link</a>";
      result = sanitizer.Sanitize (inputLegal);
      Assert.Equal (inputLegal, result);
    }

    /// <summary>
    /// Regression test for checking if relative URL's are accepted.
    /// </summary>
    [Fact]
    public void AHrefUrlCheckRelativeTest () {

      string result;
      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("a").CheckAttribute ("href", HtmlSanitizerCheckType.Url);

      // Test a relative url, which should pass.
      var input = @"<a href=""../relative.htm"">Relative link</a>";
      var expected = @"<a href=""../relative.htm"">Relative link</a>";
      result = sanitizer.Sanitize (input);
      Assert.Equal (expected, result);
    }

    /// <summary>
    /// Verifies the functioning of the URL check on src attributes.
    /// </summary>
    [Fact]
    public void ImgSrcUrlCheckTest () {

      string result;
      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("img").CheckAttribute ("src", HtmlSanitizerCheckType.Url);

      // Test some illegal href
      var inputIllegal = @"<img src=""javascript:alert('test')"">";
      var expectedIllegal = @"";
      result = sanitizer.Sanitize (inputIllegal);
      Assert.Equal (expectedIllegal, result);

      // Test a legal well formed url
      var inputLegal = @"<img src=""http://www.google.com/a.png"">";
      result = sanitizer.Sanitize (inputLegal);
      Assert.Equal (inputLegal, result);
    }

    /// <summary>
    /// Regression test for checking if relative URL's are accepted.
    /// </summary>
    [Fact]
    public void ImgSrcUrlCheckRelativeTest () {

      string result;
      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("img").CheckAttribute ("src", HtmlSanitizerCheckType.Url);

      // Test a relative url, which should pass.
      var input = @"<img src=""../relative.png"">";
      var expected = @"<img src=""../relative.png"">";
      result = sanitizer.Sanitize (input);
      Assert.Equal (expected, result);
    }

    /// <summary>
    /// Tests if empty attributes are left alone.
    /// </summary>
    [Fact]
    public void EmptyAttributeTest () {

      string result;

      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("input").AllowAttributes ("disabled", "value");

      var input = @"<input disabled value=""test"">";
      var expected = @"<input disabled="""" value=""test"">";
      result = sanitizer.Sanitize (input);
      Assert.Equal (expected, result);
    }

    /// <summary>
    /// Checks if unescaped values in attributes are correctly cleaned up.
    /// </summary>
    [Fact]
    public void UnescapedAttributeTest () {

      string result;

      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("span").AllowAttributes ("style");

      var input = @"<span style=""<strong>Whats this?</strong>"">Text</span>";
      result = sanitizer.Sanitize (input);
      Assert.Equal (input, result);
    }

    /// <summary>
    /// Tests if attribute work with data URI
    /// </summary>
    [Fact]
    public void AHrefUrlDataUriCheckTest () {

      string result;
      var sanitizer = new HtmlSanitizer ();
      sanitizer.Tag ("a").CheckAttribute ("href", HtmlSanitizerCheckType.Url);

      // Test some well formed href
      var inputLegal = @"<a href=""data:image/svg+xml;base64,UEsDBBQAAAAI"">That XSS trick</a>";
      result = sanitizer.Sanitize (inputLegal);
      Assert.Equal (inputLegal, result);

      // Test a illegal
      var inputIllegal = @"<a href=""data:html/text;base64,UEsDBBQAAAAI"">That XSS trick</a>";
      result = sanitizer.Sanitize (inputIllegal);
      Assert.Equal ("That XSS trick", result);
    }
  }
}