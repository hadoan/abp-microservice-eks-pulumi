using Pulumi;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Deployment = Pulumi.Kubernetes.Apps.V1.Deployment;

namespace Infra.Resources
{
    public abstract class BaseDeployment
    {
        protected Deployment Deploy(string name, string containerName, int containerPort)
        {
            var appLabels = new InputMap<string>{
                { "app", name },
            };

            var deployment = new Deployment(name, new DeploymentArgs
            {
                Spec = new DeploymentSpecArgs
                {
                    Selector = new LabelSelectorArgs
                    {
                        MatchLabels = appLabels,
                    },
                    Replicas = 1,
                    Template = new PodTemplateSpecArgs
                    {
                        Metadata = new ObjectMetaArgs
                        {
                            Labels = appLabels,
                        },
                        Spec = new PodSpecArgs
                        {
                            Containers =
                            {
                                new ContainerArgs
                                {
                                    Name = containerName,
                                    Image =containerName,
                                    Ports =
                                    {
                                        new ContainerPortArgs
                                        {
                                            ContainerPortValue = containerPort
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
            });

            return deployment;
        }
    }
}
