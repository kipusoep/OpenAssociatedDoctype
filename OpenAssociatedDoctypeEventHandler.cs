using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
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
            if (UmbracoContext.Current.Security.CurrentUser.AllowedSections.Contains("settings"))
            {
                MenuItem menuItem = new MenuItem("assDocType", "Open associated DocType")
                {
                    Icon = "wrench"
                };
                menuItem.AdditionalData.Add("actionRoute", string.Format("/settings/framed/%2Fumbraco%2Fsettings%2FeditNodeTypeNew.aspx%3Fid%3D{0}", ApplicationContext.Current.Services.ContentService.GetById(int.Parse(e.NodeId)).ContentTypeId));
                e.Menu.Items.Add(menuItem);
            }
        }
    }
}