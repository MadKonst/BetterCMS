﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetterCms.Module.Root.Views.Shared.Partial
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Shared/Partial/Region.cshtml")]
    public partial class Region : System.Web.Mvc.WebViewPage<BetterCms.Module.Root.ViewModels.Cms.RenderPageViewModel>
    {
        public Region()
        {
        }
        public override void Execute()
        {
WriteLiteral("<script");

WriteLiteral(" type=\"text/html\"");

WriteLiteral(" id=\"bcms-region-overlay-template\"");

WriteLiteral(">\r\n    <div class=\"bcms-region-overlay bcms-layer\">\r\n        <div class=\"bcms-reg" +
"ion-overlay-inner\">\r\n            <div class=\"bcms-hidden-overlay-bg\"></div>\r\n");

            
            #line 7 "..\..\Views\Shared\Partial\Region.cshtml"
            
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Shared\Partial\Region.cshtml"
             if (!Model.IsReadOnly && Model.HasEditRole)
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"bcms-region-actions\"");

WriteLiteral(">\r\n                    <div");

WriteLiteral(" class=\"bcms-region-button bcms-leave-child-content\"");

WriteLiteral(" style=\"display: none; background: url(\'http://i.imgur.com/10tFODt.png\') no-repea" +
"t scroll 4px 4px rgba(0, 0, 0, 0); margin-left: 4px;\"");

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" class=\"bcms-region-button bcms-region-sortdone\"");

WriteLiteral(" style=\"display: none;\"");

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" class=\"bcms-region-button bcms-region-addcontent\"");

WriteLiteral("></div>\r\n                    <div");

WriteLiteral(" class=\"bcms-region-button bcms-region-sortcontent\"");

WriteLiteral("></div>\r\n                </div>\r\n");

WriteLiteral("                <div");

WriteLiteral(" class=\"bcms-sorting-block\"");

WriteLiteral("></div>\r\n");

            
            #line 16 "..\..\Views\Shared\Partial\Region.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n</script>\r\n");

        }
    }
}
#pragma warning restore 1591
