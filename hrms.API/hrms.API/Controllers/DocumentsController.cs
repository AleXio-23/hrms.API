using hrms.Application.Services.Documents;
using hrms.Domain.Models.Dictionary.Gender;
using hrms.Domain.Models.Documents;
using hrms.Domain.Models.Shared;
using hrms.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsFacade _documentsFacade;

        public DocumentsController(IDocumentsFacade documentsFacade)
        {
            _documentsFacade = documentsFacade;
        }


        #region DocumentType CRUDs

        /// <summary>
        /// Get document type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetDocumentType")]
        public async Task<ActionResult<ServiceResult<DocumentTypeDTO>>> GetDocumentType([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _documentsFacade.GetDocumentTypeService.Execute(id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get document types list
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("GetDocumentTypes")]
        public async Task<ActionResult<ServiceResult<List<DocumentTypeDTO>>>> GetDocumentTypes([FromQuery] DocumentTypeFilter filter, CancellationToken cancellationToken)
        {
            var result = await _documentsFacade.GetDocumentTypesService.Execute(filter, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Add new or update existing document type
        /// </summary>
        /// <param name="documentTypeDTO"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdateDocumentType")]
        public async Task<ActionResult<ServiceResult<DocumentTypeDTO>>> AddOrUpdateDocumentType([FromBody] DocumentTypeDTO documentTypeDTO, CancellationToken cancellationToken)
        {
            var result = await _documentsFacade.AddOrUpdateDocumentTypeService.Execute(documentTypeDTO, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// delete document type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDocumentType")]
        public async Task<ActionResult<ServiceResult<bool>>> DeleteDocumentType([FromBody] IdRequest request, CancellationToken cancellationToken)
        {
            var result = await _documentsFacade.DeleteDocumentTypeService.Execute(request.Id, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        #endregion

        /// <summary>
        /// Upload document
        /// </summary>
        /// <param name="uploadDocumentRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("UploadDocument")]
        [Authorize]
        public async Task<ActionResult<ServiceResult<UploadDocumentResponse>>> UploadDocument([FromQuery] UploadDocumentRequest uploadDocumentRequest, CancellationToken cancellationToken)
        {
            var result = await _documentsFacade.UploadDocumentService.Execute(uploadDocumentRequest, cancellationToken).ConfigureAwait(false);
            if (result.ErrorOccured)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
