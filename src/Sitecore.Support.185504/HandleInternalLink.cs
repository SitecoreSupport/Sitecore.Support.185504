using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Sitecore.Modules.EmailCampaign.Core.Pipelines.GenerateLink;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Modules.EmailCampaign.Core;
using Sitecore.Modules.EmailCampaign.Core.Links;
using Sitecore.Sites;

namespace Sitecore.Support.Modules.EmailCampaign.Core.Pipelines.GenerateLink.Hyperlink
{
  public class HandleInternalLink: GenerateLinkProcessor
  {
    public override void Process(GenerateLinkPipelineArgs args)
    {
      Assert.IsNotNull(args, "Arguments can't be null");
      Assert.IsNotNull(args.Url, "Url link can't be null");
      if (LinksManager.IsAbsoluteLink(args.Url))
      {
        if (args.Url == "link:")
        {
          args.AbortPipeline();
        }
        return;
      }
      if (Regex.IsMatch(args.Url, "^([a-zA-Z0-9+.-]+:)"))
      {
        args.AbortPipeline();
        return;
      }

      if (args.Url.IndexOf('/') != 0)
      {
        args.Url = args.Url.Insert(0, "/");
      }
    }
  }
}