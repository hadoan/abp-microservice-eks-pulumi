using Pulumi.Kubernetes.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Secrets
{
    public class SqlSAPwdSecret
    {
        public Secret Deploy()
        {
            var secret = new Secret("mssql", new SecretArgs
            {
                Metadata =new ObjectMetaArgs{
                    Name="mssql"
                },
                Type = "Opaque",
                Data = new Pulumi.InputMap<string> { { "SA_PASSWORD", "MTIzNDU2" } }
            });
            return secret;
        }
    }
}
