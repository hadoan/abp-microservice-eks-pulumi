using Pulumi;
using Pulumi.Kubernetes.Apps.V1;
using Pulumi.Kubernetes.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Services
{
    public abstract class BaseService
    {
        public Service Deploy(string appName, Pulumi.Kubernetes.Apps.V1.Deployment deployment, bool isLoadBalancer, InputMap<string> appLabels)
        {
            var service = new Service(appName, new ServiceArgs
            {
                Metadata = new ObjectMetaArgs
                {
                    Labels = deployment.Spec.Apply(spec =>
                        spec.Template.Metadata.Labels
                   ),
                },
                Spec = new ServiceSpecArgs
                {
                    Type = isLoadBalancer
                       ? "LoadBalancer"
                       : "ClusterIP",
                    Selector = appLabels,
                    Ports = new ServicePortArgs
                    {
                        Port = 80,
                        TargetPort = 80,
                        Protocol = "TCP",
                    },
                }
            });

            return service;
        }
    }
}
