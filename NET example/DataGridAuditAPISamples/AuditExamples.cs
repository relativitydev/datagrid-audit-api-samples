﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kCura.AuditUI2.Services.ExternalAuditLog;
using Relativity.Services;
using Relativity.Services.Objects;
using Relativity.Services.Objects.DataContracts;
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
            string workspaceName = "Admin Case";

            int choiceId = await GetChoiceId(objMgr, workspaceId, workspaceName);
            var choiceSelected = new List<int> { choiceId };

            // create the condition
            var condition = new SingleChoiceCondition("Workspace Name", SingleChoiceConditionEnum.AnyOfThese, choiceSelected);

            // list fields
            var fields = new List<Relativity.Services.Objects.DataContracts.FieldRef>
            {
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = "Audit ID"
                },
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = "Timestamp"
                },
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = "Action"
                },
                new Relativity.Services.Objects.DataContracts.FieldRef
                {
                    Name = "Workspace Name"
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

            Relativity.Services.Objects.DataContracts.QueryResult res
                = await logMgr.QueryAsync(workspaceId, queryRequest, start, length);

            // do something with results

            Console.WriteLine($"Number of objects returned: {res.TotalCount}");
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
