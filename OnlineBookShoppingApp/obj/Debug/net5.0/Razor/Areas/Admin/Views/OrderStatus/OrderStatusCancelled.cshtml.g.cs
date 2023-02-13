#pragma checksum "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7a1f722bec7ebf8f93bc1c6c975e935fb818bca2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_OrderStatus_OrderStatusCancelled), @"mvc.1.0.view", @"/Areas/Admin/Views/OrderStatus/OrderStatusCancelled.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\_ViewImports.cshtml"
using OnlineBookShoppingApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\_ViewImports.cshtml"
using OnlineBookShoppingApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7a1f722bec7ebf8f93bc1c6c975e935fb818bca2", @"/Areas/Admin/Views/OrderStatus/OrderStatusCancelled.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7bfb813aff51ca13563ffe4879e099f64fb4199a", @"/Areas/Admin/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Admin_Views_OrderStatus_OrderStatusCancelled : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OnlineBookShoppingApp.Models.OrderHeader>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h2 class=\"text-primary text-center\">Order Status</h2>\r\n");
            WriteLiteral(@"<table class=""table table-striped table-bordered table-active table-dark bg-dark text-white"">
    <thead>
        <tr>
            <th>Name</th>
            <th>OrderDate</th>
            <th>OrderStatus</th>
            <th>PaymentStatus</th>
            <th>PaymentDate</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 16 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
         foreach (var item in Model)
        {
            using (Html.BeginForm("Details", "OrderStatus", new { id = item.Id }))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <th>");
#nullable restore
#line 21 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
                   Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 22 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
                   Write(item.OrderDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 23 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
                   Write(item.OrderStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 24 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
                   Write(item.PaymentStatus);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n                    <th>");
#nullable restore
#line 25 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
                   Write(item.PaymentDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</th>\r\n\r\n\r\n                    <td>\r\n\r\n                        ");
#nullable restore
#line 30 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
                   Write(Html.ActionLink("Details", "Details", new { id = item.Id }, new { @class = "btn btn-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 34 "D:\GitHubProject\BookShop\OnlineBookShoppingApp\OnlineBookShoppingApp\Areas\Admin\Views\OrderStatus\OrderStatusCancelled.cshtml"
            }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n\r\n</table>\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OnlineBookShoppingApp.Models.OrderHeader>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
