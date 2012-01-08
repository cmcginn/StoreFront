using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Authentication.External;
using Nop.Services.Localization;

namespace Nop.Plugin.ExternalAuth.Facebook
{
    /// <summary>
    /// Facebook externalAuth processor
    /// </summary>
    public class FacebookExternalAuthMethod : BasePlugin, IExternalAuthenticationMethod
    {
        #region Fields
        private readonly FacebookExternalAuthSettings _facebookExternalAuthSettings;
        #endregion

        #region Ctor

        public FacebookExternalAuthMethod(FacebookExternalAuthSettings facebookExternalAuthSettings)
        {
            this._facebookExternalAuthSettings = facebookExternalAuthSettings;
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ExternalAuthFacebook";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.ExternalAuth.Facebook.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Gets a route for displaying plugin in public store
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetPublicInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "ExternalAuthFacebook";
            routeValues = new RouteValueDictionary() { { "Namespaces", "Nop.Plugin.ExternalAuth.Facebook.Controllers" }, { "area", null } };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Facebook.ClientKeyIdentifier", "Client key identifier");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Facebook.ClientKeyIdentifier.Hint", "Enter your client key identifier here.");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Facebook.ClientSecret", "Client secret");
            this.AddOrUpdatePluginLocaleResource("Plugins.ExternalAuth.Facebook.ClientSecret.Hint", "Enter your client secret here.");

            base.Install();
        }
        #endregion
        
    }
}
