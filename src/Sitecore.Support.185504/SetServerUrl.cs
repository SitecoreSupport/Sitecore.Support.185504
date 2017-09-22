using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GenerateLink;

namespace Sitecore.Support.Modules.EmailCampaign.Core.Pipelines.GenerateLink
{
  public class SetServerUrl: GenerateLinkProcessor
  {
    public override void Process(GenerateLinkPipelineArgs args)
    {
      Assert.IsNotNull(args, "Arguments can't be null");
      UrlOptions options = LinkManager.GetDefaultUrlOptions();
      options.AlwaysIncludeServerUrl = true;
      options.SiteResolving = true;
      options.Site = Sitecore.Configuration.Factory.GetSite(args.WebsiteConfigurationName);
      string itemUrl = LinkManager.GetItemUrl(args.MailMessage.InnerItem, options);
      args.ServerUrl = new Uri(itemUrl, UriKind.Absolute).GetLeftPart(UriPartial.Authority);
    }
  }
}