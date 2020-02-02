using Pulumi;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Deployment = Pulumi.Kubernetes.Apps.V1.Deployment;

namespace Infra.Resources
{
    public class AuthServerHostDeployment:BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("auth-server", "auth-server", 64999);
        }
    }

    public class IdentityServiceHostDeployment : BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("identityservice-host", "identityservice-host", 63568);
        }
    }

    public class BloggingServiceHostDeployment:BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("bloggingservice-host", "bloggingservice-host", 62157);
        }
    }

    public class ProductServiceHostDeployment:BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("productservice-host", "productservice-host", 60244);
        }
    }

    public class InternalGatewayHostDeployment : BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("internalgateway-host", "internalgateway-host", 65129);
        }
    }
    public class BackendAdminAppGatewayHostDeployment : BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("backendadminappgateway-host", "backendadminappgateway-host", 65115);
        }
    }

    public class PublicWebSiteGatewayHostDeployment : BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("publicwebsitegateway-host", "publicwebsitegateway-host", 64897);
        }
    }

    public class BackendAdminAppHostDeployment : BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("backendadminapp-host", "backendadminapp-host", 51954);
        }
    }

    public class PublicWebSiteHostDeployment : BaseDeployment
    {
        public Deployment New()
        {
            return Deploy("publicwebsite-host", "publicwebsite-host", 53435);
        }
    }

}
