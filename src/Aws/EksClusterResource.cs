//using System;
//using System.Collections.Generic;
//using System.Text;
//using Pulumi.Aws.Eks;
//using Pulumi.Aws.Eks.Inputs;

//namespace Infra.Aws
//{
//    public class EksClusterResource
//    {
//        public Cluster InitCluster()
//        {
          
//            return new Cluster("dev", new ClusterArgs
//            {
//                Name="dev",
//                RoleArn= "arn:aws:iam::979724264673:role/oversea-cluster-role",
//                VpcConfig = new ClusterVpcConfigArgs
//                {
                    
//                }
//            });
//        }
//    }
//}
