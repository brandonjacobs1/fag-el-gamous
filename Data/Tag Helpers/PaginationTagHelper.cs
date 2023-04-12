//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using fag_el_gamous.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace fag_el_gamous.TagHelpers
//{
//    [HtmlTargetElement("div", Attributes = "page-blah")]
//    public class PaginationTagHelper : TagHelper
//    {
//        private IUrlHelperFactory _uhf;

//        public PaginationTagHelper(IUrlHelperFactory uhf)
//        {
//            _uhf = uhf;
//        }
//        [ViewContext]
//        [HtmlAttributeNotBound]
//        public ViewContext vc { get; set; }
//        public PageInfo PageBlah { get; set; }
//        public string PageAction { get; set; }
//        public bool PageClassesEnabled { get; set; } = false;
//        public string PageClass { get; set; }
//        public string PageClassNormal { get; set; }
//        public string PageClassSelected { get; set; }
//        public override void Process(TagHelperContext thc, TagHelperOutput tho)
//        {
//            IUrlHelper uh = _uhf.GetUrlHelper(vc);

//            TagBuilder final = new TagBuilder("div");

//            for (int i = 1; i <= PageBlah.NumPages; i++)
//            {
//                TagBuilder tb = new TagBuilder("a");
//                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });
//                if (PageClassesEnabled)
//                {
//                    tb.AddCssClass(PageClass);
//                    tb.AddCssClass(i == PageBlah.CurrentPage ? PageClassSelected : PageClassNormal);
//                }
//                tb.InnerHtml.Append(i.ToString());
//                final.InnerHtml.AppendHtml(tb);
//            }
//            tho.Content.AppendHtml(final);
//        }
//    }
//}



