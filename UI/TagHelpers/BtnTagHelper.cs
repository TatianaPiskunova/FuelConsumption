using Microsoft.AspNetCore.Razor.TagHelpers;

namespace UI.TagHelpers
{
    public class BtnTagHelper : TagHelper
    {
        public string Type { get; set; } = "Submit";

        public string BgColor { get; set; } = "success";

        public string Text { get; set; } = "Отправить";

        public string Class { get; set; }

        public string OnClick { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", $"btn btn-{BgColor} {Class}");

            if (!string.IsNullOrEmpty(OnClick))
            {
                output.Attributes.SetAttribute("onclick", OnClick);
            }

            output.Attributes.SetAttribute("type", Type);
            output.Content.SetContent(Text);
        }
    }
}
