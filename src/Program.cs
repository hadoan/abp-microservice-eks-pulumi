using System.Collections.Generic;
using System.Threading.Tasks;

using Pulumi;
using Pulumi.Kubernetes.Core.V1;
using Pulumi.Kubernetes.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Infra.Secrets;
using Infra.Databases;
using Infra.Services;
using Infra.Volumes;

class Program
{
    static Task<int> Main()
    {
        return Pulumi.Deployment.RunAsync(() =>
        {

            //create volume
            var volumeClaim = new MsSqlPersistentVolumeClaim().Deploy();

            //create new secret for SA account
            var secret = new SqlSAPwdSecret().Deploy();
            //Deploy sql server
            var sqlName = "mssqldb";
            var appLabels = new Pulumi.InputMap<string>{
                { "app", sqlName },
            };

            var deployment = new SqlServerDeployment().Deploy(sqlName, appLabels);
            //Deploy service
            var sqlService = new SqlServerService().Deploy("mssql-service", deployment, appLabels);

            return new Dictionary<string, object?>
            {

                { "secret", secret.GetResourceName() },
                { "deployment", deployment.GetResourceName() },
                { "service", sqlService.GetResourceName() },
            };
        });
    }
}
