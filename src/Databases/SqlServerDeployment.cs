using Pulumi;
using Pulumi.Kubernetes.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Apps.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using System;
using System.Collections.Generic;
using System.Text;
using Deployment = Pulumi.Kubernetes.Apps.V1.Deployment;


namespace Infra.Databases
{
    //https://docs.microsoft.com/en-us/sql/linux/tutorial-sql-server-containers-kubernetes?view=sql-server-ver15

    public class SqlServerDeployment
    {
        public Deployment Deploy(string name, InputMap<string> appLabels)
        {
           
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
                                    Name = "mssql",
                                    Image = "mcr.microsoft.com/mssql/server:2017-CU11-ubuntu",
                                    Ports =
                                    {
                                        new ContainerPortArgs
                                        {
                                            ContainerPortValue = 1433
                                        },
                                    },
                                    Env =new List<EnvVarArgs>
                                    {
                                        new EnvVarArgs
                                        {
                                            Name="ACCEPT_EULA",
                                            Value="Y"
                                        },
                                        new EnvVarArgs
                                        {
                                            Name="SA_PASSWORD",
                                            ValueFrom = new EnvVarSourceArgs
                                            {
                                                SecretKeyRef = new SecretKeySelectorArgs
                                                {
                                                    Name="mssql",
                                                    Key="SA_PASSWORD"
                                                }
                                            }
                                        },

                                    },
                                    VolumeMounts = new VolumeMountArgs{
                                        Name="mssqldb",
                                        MountPath="/var/opt/mssql"
                                    }
                                },
                            },
                            Volumes = new List<VolumeArgs>
                            {
                                new VolumeArgs
                                {
                                    Name="mssqldb",
                                    PersistentVolumeClaim=new PersistentVolumeClaimVolumeSourceArgs
                                    {
                                        ClaimName ="mssql-volume-claim"
                                    },
                                    
                                }
                            }
                        },
                    },
                },

            });

            return deployment;
        }
    }
}
