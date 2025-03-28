﻿using cheez_ims_api.Data;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace cheez_ims_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(OperationId = "GetHealth", Summary = "Get Health", Tags = new[] { "Health" })]
        public async Task<IResult> GetHealth()
        {
            try
            {
                await _context.Database.CanConnectAsync();
                return Results.Ok("Database is healthy!");
            } catch (Exception ex)
            {
                return Results.Problem($"Database connection failed: {ex.Message}");
            }
        }
    }
}
