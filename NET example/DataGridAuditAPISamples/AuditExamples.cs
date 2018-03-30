using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.ExternalAuditLog;
using Relativity.Services;
using Relativity.Services.Objects;
using Relativity.Services.Objects.DataContracts;
using Relativity.Services.Objects.Models;
using FieldValuePair = Relativity.Services.Field.FieldValuePair;
using RelativityObject = Relativity.Services.Objects.DataContracts.RelativityObject;
using Sort = Relativity.Services.Objects.DataContracts.Sort;

namespace DataGridAuditAPISamples
{
    public static class AuditExamples
    {
        /// <summary>
        /// Queries the first 100 admin case audits
        /// </summary>
        /// <param name="logMgr"></param>
        /// <param name="objMgr"></param>
        /// <param name="workspaceId"></param>
        /// <returns></returns>
        public static async Task QueryAdminAudits(
            IExternalAuditLogObjectManager logMgr, 
            IObjectManager objMgr, 
            int workspaceId = -1)
        {
            const int start = 1;
            const int length = 100;

            // filter for admin actions only
            const string workspaceName = "Admin Case";

            int choiceId = await GetChoiceId(objMgr, workspaceId, workspaceName);
            var choiceSelected = new List<int> { choiceId };

            // create the condition
            var condition = new SingleChoiceCondition(
                Constants.Names.Fields.WorkspaceName, 
                SingleChoiceConditionEnum.AnyOfThese, 
                choiceSelected);


            #region New version - specifying fields and query

            // list fields
            var fields = new List<Relativity.Services.Objects.DataContracts.FieldRef>
            {
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = Constants.Names.Fields.AuditId
                },
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = Constants.Names.Fields.Timestamp
                },
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = Constants.Names.Fields.Action
                },
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = Constants.Names.Fields.WorkspaceName
                }
            };

            var queryRequest = new Relativity.Services.Objects.DataContracts.QueryRequest
            {
                Condition = condition.ToQueryString(),
                Fields = fields,
                Sorts = new List<Sort>(),
                IncludeIDWindow = true,
                RowCondition = ""
            };

            #endregion


            #region Older (currently deployed) version (9.6.26.97)

            var fieldsOld = new List<Relativity.Services.Field.FieldRef>
            {
                new Relativity.Services.Field.FieldRef
                {
                    Name = Constants.Names.Fields.AuditId
                },
                new Relativity.Services.Field.FieldRef
                {
                    Name = Constants.Names.Fields.Timestamp
                }
            };

            var query = new Relativity.Services.Objects.Models.Query
            {
                Fields = fieldsOld,
                Condition = condition.ToQueryString()
            };
            
            var artifactType = new Relativity.Services.ObjectTypeReference.ObjectTypeRef
            {
                DescriptorArtifactTypeID = Constants.Nums.AuditDescriptorId // can hard-code this, I think
            };

            #endregion

            ObjectQueryResultSet res
                = await logMgr.QueryAsync(workspaceId, artifactType, query, start, length);

            // do something with results

            foreach (Result<Relativity.Services.Objects.Models.RelativityObject> record in res.Results)
            {
                IEnumerable<FieldValuePair> fieldValuePairs = record.Artifact.FieldValuePairs;
                foreach (FieldValuePair fieldValuePair in fieldValuePairs)
                {
                    Console.WriteLine("Field: {0}", fieldValuePair.Field.Name);
                    Console.WriteLine("Value: {0}", fieldValuePair.Value);
                }
                Console.WriteLine("---");
            }

            Console.WriteLine($"Total number of objects returned: {res.TotalCount}");
        }


        private static async Task<int> GetChoiceId(IObjectManager mgr, int workspaceId, string choiceName)
        {
            int result = -1;

            var condition = new TextCondition("Name", TextConditionEnum.Like, choiceName);
            QueryRequest request = new QueryRequest
            {
                ObjectType = new ObjectTypeRef { ArtifactTypeID = 7 },
                Condition = condition.ToQueryString(),
                Fields = new List<FieldRef>()
            };

            QueryResult response = await mgr.QueryAsync(workspaceId, request, 1, 1);
            if (response.ResultCount > 0)
            {
                RelativityObject firstResult = response.Objects.FirstOrDefault();
                result = firstResult.ArtifactID;
            }

            return result;
        }
    }
}
