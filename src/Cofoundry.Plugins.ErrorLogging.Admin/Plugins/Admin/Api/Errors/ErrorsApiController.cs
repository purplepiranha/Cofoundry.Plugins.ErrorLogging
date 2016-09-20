﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Cofoundry.Domain.CQS;
using Cofoundry.Web.WebApi;
using Cofoundry.Web.Admin;
using Cofoundry.Plugins.ErrorLogging.Domain;

namespace Cofoundry.Plugins.ErrorLogging.Admin
{
    [RoutePrefix(RouteConstants.PluginApiRoutePrefix + "/Errors")]
    public class ErrorsApiController : BaseAdminApiController
    {
        #region private member variables

        private const string ID_ROUTE = "{errorId:int}";

        private readonly IQueryExecutor _queryExecutor;
        private readonly ApiResponseHelper _apiResponseHelper;

        #endregion

        #region constructor

        public ErrorsApiController(
            IQueryExecutor queryExecutor,
            ApiResponseHelper apiResponseHelper
            )
        {
            _queryExecutor = queryExecutor;
            _apiResponseHelper = apiResponseHelper;
        }

        #endregion

        #region routes

        #region queries

        [HttpGet]
        [Route]
        public async Task<IHttpActionResult> Get([FromUri] SearchErrorSummariesQuery query)
        {
            if (query == null) query = new SearchErrorSummariesQuery();

            var results = await _queryExecutor.ExecuteAsync(query);
            return _apiResponseHelper.SimpleQueryResponse(this, results);
        }
        
        [HttpGet]
        [Route(ID_ROUTE)]
        public async Task<IHttpActionResult> Get(int errorId)
        {
            var result = await _queryExecutor.GetByIdAsync<ErrorDetails>(errorId);
            return _apiResponseHelper.SimpleQueryResponse(this, result);
        }

        #endregion

        #endregion
    }
}