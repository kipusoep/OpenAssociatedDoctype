using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models.Trees;
using Umbraco.Web.Trees;

namespace InfoCaster.Umbraco.OpenAssociatedDoctype
{
    public class OpenAssociatedDoctypeEventHandler : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            ContentTreeController.MenuRendering += ContentTreeController_MenuRendering;
        }

        void ContentTreeController_MenuRendering(TreeControllerBase sender, MenuRenderingEventArgs e)
        {
            int nodeId;
            if (int.TryParse(e.NodeId, out nodeId) && nodeId > -1 && UmbracoContext.Current.Security.CurrentUser.AllowedSections.Contains("settings"))
            {
                IContent content = ApplicationContext.Current.Services.ContentService.GetById(nodeId);
                if (content != null)
                {
                    MenuItem menuItem = new MenuItem("assDocType", "Open associated DocType")
                    {
                        Icon = "wrench"
                    };
                    menuItem.AdditionalData.Add("actionRoute", string.Format("settings/framed/settings%2FeditNodeTypeNew.aspx%3Fid%3D{0}", content.ContentTypeId));
                    e.Menu.Items.Add(menuItem);
                }
            }
        }
    }
}