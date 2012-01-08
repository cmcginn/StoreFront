﻿using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.WebServices.Security;
using Nop.Services.Common;
using Nop.Services.Security;

namespace Nop.Plugin.Misc.WebServices
{
    public class WebServicePlugin : BasePlugin, IMiscPlugin
    {
        #region Ctor

        private readonly IPermissionService _permissionService;

        #endregion

        #region Ctor

        public WebServicePlugin(IPermissionService permissionService)
        {
            this._permissionService = permissionService;
        }

        #endregion

        #region Methods

        public override void Install()
        {
            //install new permissions
            _permissionService.InstallPermissions(new WebServicePermissionProvider());

            base.Install();
        }

        public override void Uninstall()
        {
            //uninstall permissions
            _permissionService.UninstallPermissions(new WebServicePermissionProvider());

            base.Uninstall();
        }

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            //configuration is not required
            actionName = null;
            controllerName = null;
            routeValues = null;
        }

        #endregion
    }
}
