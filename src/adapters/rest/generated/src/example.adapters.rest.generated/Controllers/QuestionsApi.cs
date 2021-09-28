/*
 * Contact center service
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 0.0.1
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using example.adapters.rest.generated.Attributes;
using example.adapters.rest.generated.Models;

namespace example.adapters.rest.generated.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class QuestionsApiController : ControllerBase
    { 
        /// <summary>
        /// Ends the answer question process.
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="endQuestion"></param>
        /// <response code="202">Accepted</response>
        [HttpPut]
        [Route("/api/questions/{question_id}/end")]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract Task<IActionResult> EndQuestion([FromRoute (Name = "question_id")][Required]Guid questionId, [FromBody]EndQuestion endQuestion);

        /// <summary>
        /// Gets a list of answer question processes.
        /// </summary>
        /// <param name="offset">The number of items to skip before starting to collect the result set</param>
        /// <param name="limit">The numbers of items to return</param>
        /// <response code="200">OK</response>
        [HttpGet]
        [Route("/api/questions")]
        [ValidateModelState]
        [ProducesResponseType(statusCode: 200, type: typeof(Questions))]
        public abstract Task<IActionResult> GetQuestions([FromQuery (Name = "offset")]int? offset, [FromQuery (Name = "limit")]int? limit);

        /// <summary>
        /// Starts the answer question process.
        /// </summary>
        /// <param name="registerQuestion"></param>
        /// <response code="202">Accepted</response>
        [HttpPost]
        [Route("/api/questions")]
        [Consumes("application/json")]
        [ValidateModelState]
        public abstract Task<IActionResult> RegisterQuestion([FromBody]RegisterQuestion registerQuestion);
    }
}
