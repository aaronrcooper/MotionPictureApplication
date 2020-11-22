using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MotionPictureDataManagement.API;
using Microsoft.Extensions.Logging;
using MotionPictureDataManagement.API.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MotionPictureDataManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotionPictureController : ControllerBase
    {
        private readonly ILogger<MotionPictureController> _logger;
        private readonly IDbConnection connection;

        public MotionPictureController(ILogger<MotionPictureController> logger,
            IDbConnection connection)
        {
            _logger = logger;
            this.connection = connection;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotionPicture), 200)]
        public IActionResult GetMotionPictureById(int id)
        {
            MotionPicture result;
            var query = "SELECT * FROM MotionPictures WHERE Id = @Id";
            try
            {

                result = connection.QuerySingleOrDefault<MotionPicture>(query, new { Id = id });

                if (result == null)
                {
                    return NotFound();
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(MotionPicture), 200)]
        public async Task<IActionResult> PostMotionPicture([FromBody] MotionPicture model)
        {
            MotionPicture result;
            var query = "INSERT INTO MotionPictures (Name, Description, ReleaseYear) "
                + "VALUES (@Name, @Description, @ReleaseYear); SELECT LAST_INSERT_ID();";
            try
            {
                var dbResult = await connection.QueryAsync<int>(query,
                    new { Name = model.Name, Description = model.Description, ReleaseYear = model.ReleaseYear });
                var lastId = dbResult.First();
                result = new MotionPicture {
                        Id = lastId,
                        Name = model.Name,
                        Description = model.Description,
                        ReleaseYear = model.ReleaseYear
                    };
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(MotionPicture), 200)]
        public IActionResult PutMotionPicture(int id, [FromBody] MotionPicture model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            MotionPicture result;
            var query = "UPDATE MotionPictures SET Name = @Name, Description = @Description, ReleaseYear = @ReleaseYear "
                        + "OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.ReleaseYear, INSERTED.Description "
                        + "WHERE Id=@Id";
            try
            {
                result = connection.QuerySingle<MotionPicture>(query,
                    new { Id = model.Id, Name = model.Name, Description = model.Description, ReleaseYear = model.ReleaseYear });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMotionPicture(int id)
        {
            var query = "DELETE FROM MotionPictures WHERE Id = @Id";
            try
            {
                var row = connection.Execute(query,
                    new { Id = id });
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

            return Ok();
        }
    }
}
