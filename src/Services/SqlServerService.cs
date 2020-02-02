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
    public class SqlServerService
    {
        public Service Deploy(string appName, Pulumi.Kubernetes.Apps.V1.Deployment deployment, InputMap<string> appLabels)
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
                    Type = "LoadBalancer",
                    Selector = appLabels,
                    Ports = new ServicePortArgs
                    {
                        Port = 1433,
                        TargetPort = 1433,
                        Protocol = "TCP",
                    },
                }
            });

            return service;
        }

    }
}
