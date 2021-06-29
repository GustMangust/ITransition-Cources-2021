using Markdig;

namespace CourceProject.Utility {
  public static class MarkDownParser {
    public static string Parse(string markdown) {
      if(string.IsNullOrEmpty(markdown))
        return "";

      var pipeline = new MarkdownPipelineBuilder()
          .UseAdvancedExtensions()
          .Build();

      return Markdown.ToHtml(markdown, pipeline);
    }

  }
}
