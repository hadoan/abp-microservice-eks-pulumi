using Pulumi.Kubernetes.Core.V1;
using Pulumi.Kubernetes.Storage.V1;
using Pulumi.Kubernetes.Types.Inputs.Core.V1;
using Pulumi.Kubernetes.Types.Inputs.Meta.V1;
using Pulumi.Kubernetes.Types.Inputs.Storage.V1;
using Pulumi.Kubernetes.Types.Outputs.Meta.V1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Volumes
{
    public class MsSqlPersistentVolumeClaim
    {
        public PersistentVolumeClaim Deploy()
        {
            var inputMap = new Pulumi.InputMap<string>();
            inputMap.Add("type", "gp2");
            inputMap.Add("fsType", "ext4");
            var storageClass = new StorageClass("storage-sql-data", new StorageClassArgs
            {
                Metadata=new ObjectMetaArgs
                {
                    Name = "storage-sql-data",
                    Annotations=new Pulumi.InputMap<string> { { "storageclass.kubernetes.io/is-default-class","true" } }
                },

                Provisioner = "kubernetes.io/aws-ebs"
            });

            var volumneClaim = new PersistentVolumeClaim("mssql-volumneclaim", new PersistentVolumeClaimArgs
            {
                Metadata = new ObjectMetaArgs
                {
                    Name = "mssql-volume-claim"
                },
                Spec = new PersistentVolumeClaimSpecArgs
                {
                    StorageClassName = "storage-sql-data",
                    AccessModes = "ReadWriteOnce",
                    Resources = new ResourceRequirementsArgs
                    {
                        Requests = new Pulumi.InputMap<string> { { "storage", "7Gi" } }
                    }
                },
            });

            return volumneClaim;
        }
    }




}
