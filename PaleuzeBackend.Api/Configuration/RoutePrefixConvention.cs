using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace PaleuzeBackend.Api.Configuration
{
    public class RoutePrefixConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _prefix;

        public RoutePrefixConvention(string prefix)
        {
            this._prefix = new AttributeRouteModel(new RouteAttribute(prefix));
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                foreach (var selector in controller.Selectors)
                {
                    if (selector.AttributeRouteModel != null)
                    {
                        selector.AttributeRouteModel =
                            AttributeRouteModel.CombineAttributeRouteModel(
                                _prefix,
                                selector.AttributeRouteModel);
                    }
                }
            }
        }
    }
}