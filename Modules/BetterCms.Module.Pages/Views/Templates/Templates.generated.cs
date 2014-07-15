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

namespace BetterCms.Module.Pages.Views.Templates
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
    
    #line 1 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Pages.Content.Resources;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Pages.Controllers;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Pages.ViewModels.SiteSettings;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Root;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Root.Content.Resources;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Root.Mvc;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Root.Mvc.Grids.Extensions;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Root.Mvc.Grids.TableRenderers;
    
    #line default
    #line hidden
    
    #line 9 "..\..\Views\Templates\Templates.cshtml"
    using BetterCms.Module.Root.Mvc.Helpers;
    
    #line default
    #line hidden
    
    #line 10 "..\..\Views\Templates\Templates.cshtml"
    using Microsoft.Web.Mvc;
    
    #line default
    #line hidden
    
    #line 12 "..\..\Views\Templates\Templates.cshtml"
    using MvcContrib.UI.Grid;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Templates/Templates.cshtml")]
    public partial class Templates : System.Web.Mvc.WebViewPage<SiteSettingTemplateListViewModel>
    {
        public Templates()
        {
        }
        public override void Execute()
        {
WriteLiteral("\r\n");

WriteLiteral("\r\n");

            
            #line 16 "..\..\Views\Templates\Templates.cshtml"
  
    var controller = (CmsControllerBase)ViewContext.Controller;
    var roles = string.Format("{0},{1},{2}",
        RootModuleConstants.UserRoles.PublishContent,
        RootModuleConstants.UserRoles.EditContent,
        RootModuleConstants.UserRoles.DeleteContent);
    var canViewPages = controller.SecurityService.IsAuthorized(roles);
    
    Action<ColumnBuilder<SiteSettingTemplateItemViewModel>> columns = column =>
    {
        column.EditButtonColumn(renderId:false);

        column.For(f => string.Format("<a class=\"bcms-tables-link bcms-grid-item-edit-button bcms-template-name\" data-id=\"{0}\">{1}</a>", f.Id, f.TemplateName))
            .Named(PagesGlobalization.SiteSettings_Templates_NameColumn)
            .SortColumnName("TemplateName")
            .Encode(false);

        if (canViewPages)
        {
            column.For(f => string.Format("<a class=\"bcms-template-usage\" data-id=\"{0}\">{1}</a>", f.Id, PagesGlobalization.SiteSettings_Templates_Usage))
                .HeaderAttributes(@style => "width: 100px;")
                .Named("")
                .Sortable(false)
                .Encode(false);
        }
        
        column.DeleteButtonColumn(false);
    };

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");

            
            #line 46 "..\..\Views\Templates\Templates.cshtml"
 using (Html.BeginForm<TemplatesController>(f => f.Templates(null), FormMethod.Post, new { @id = "bcms-templates-form", @class = "bcms-ajax-form" }))
{
    
            
            #line default
            #line hidden
            
            #line 48 "..\..\Views\Templates\Templates.cshtml"
Write(Html.HiddenGridOptions(Model.GridOptions));

            
            #line default
            #line hidden
            
            #line 48 "..\..\Views\Templates\Templates.cshtml"
                                              
    

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"bcms-top-block-holder\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"bcms-btn-links-main\"");

WriteLiteral(" id=\"bcms-register-template-button\"");

WriteLiteral(">");

            
            #line 51 "..\..\Views\Templates\Templates.cshtml"
                                                                       Write(PagesGlobalization.SiteSettings_Widgets_RegisterNew);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n        <div");

WriteLiteral(" class=\"bcms-search-block\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 53 "..\..\Views\Templates\Templates.cshtml"
       Write(Html.TextBoxFor(m => m.SearchQuery, new { @class = "bcms-editor-field-box bcms-search-query", @placeholder = RootGlobalization.WaterMark_Search }));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"bcms-btn-search\"");

WriteLiteral(" id=\"bcms-template-search-btn\"");

WriteLiteral(">");

            
            #line 54 "..\..\Views\Templates\Templates.cshtml"
                                                                  Write(PagesGlobalization.SiteSettings_Template_Search);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n        </div>\r\n        <div");

WriteLiteral(" class=\"bcms-featured-grid bcms-clearfix\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 57 "..\..\Views\Templates\Templates.cshtml"
       Write(Html.RenderPaging(Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>    \r\n");

            
            #line 60 "..\..\Views\Templates\Templates.cshtml"
    
    
            
            #line default
            #line hidden
            
            #line 61 "..\..\Views\Templates\Templates.cshtml"
Write(Html.SiteSettingsMessagesBox());

            
            #line default
            #line hidden
            
            #line 61 "..\..\Views\Templates\Templates.cshtml"
                                   

    
            
            #line default
            #line hidden
            
            #line 63 "..\..\Views\Templates\Templates.cshtml"
Write(Html
          .Grid(Model.Items)
          .Sort(Model.GridOptions)
          .Columns(columns)
          .Attributes(@class => "bcms-tables")          
          .RowAttributes(delegate(GridRowViewData<SiteSettingTemplateItemViewModel> row)
                  {
                      return new Dictionary<string, object>
                              {
                                    {"data-id", row.Item.Id},
                                    {"data-version", row.Item.Version}
                              };
              })
              .RenderUsing(new EditableHtmlTableGridRenderer<SiteSettingTemplateItemViewModel>()));

            
            #line default
            #line hidden
            
            #line 76 "..\..\Views\Templates\Templates.cshtml"
                                                                                                  
}

            
            #line default
            #line hidden
WriteLiteral("\r\n<script");

WriteLiteral(" type=\"text/html\"");

WriteLiteral(" id=\"bcms-template-list-row-template\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 80 "..\..\Views\Templates\Templates.cshtml"
Write(Html
        .Grid(new List<SiteSettingTemplateItemViewModel> { new SiteSettingTemplateItemViewModel() })
        .Columns(columns)
        .Attributes(@class => "bcms-tables")
        .RenderUsing(new HtmlTableGridSingleRowRenderer<SiteSettingTemplateItemViewModel>()));

            
            #line default
            #line hidden
WriteLiteral("\r\n</script>\r\n\r\n");

        }
    }
}
#pragma warning restore 1591