using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Mappings;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public TransactionsController(ILogger<TransactionsController> logger, IMapper mapper,
            ITransactionService transactionService)
        {
            _logger = logger;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> GetById(Guid id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            if (transaction is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TransactionDto>(transaction));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<TransactionDto>>> GetAll()
        {
            var transactions = await _transactionService.GetTransactionsAsync();

            return Ok(_mapper.Map<IList<TransactionDto>>(transactions));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<Transaction>>> Create(TransactionBody transactionBody)
        {
            var transaction = await _transactionService.CreateTransactionAsync(transactionBody);

            return Created($"/api/transactions/{transaction.Id}", _mapper.Map<TransactionDto>(transaction));
        }
    }
}
