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

namespace MotionPictureDataManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotionPictureController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<MotionPictureController> _logger;
        private readonly IConfiguration _config;

        public MotionPictureController(ILogger<MotionPictureController> logger, 
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _connectionString = _config.GetConnectionString("MotionPictureDb");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotionPicture), 200)]
        public IActionResult GetMotionPictureById(int id)
        {
            MotionPicture result;
            var query = "SELECT * FROM MotionPictures WHERE Id = @Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    result = connection.QuerySingleOrDefault<MotionPicture>(query, new { Id = id });

                    if (result == null)
                    {
                        return NotFound();
                    }
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
        public IActionResult PostMotionPicture([FromBody] MotionPicture model)
        {
            MotionPicture result;
            var query = "INSERT INTO MotionPictures (Name, Description, ReleaseYear) OUTPUT INSERTED.Id, "
                +"INSERTED.Name, INSERTED.ReleaseYear, INSERTED.Description VALUES (@Name, @Description, @ReleaseYear)";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    result = connection.QuerySingle<MotionPicture>(query,
                        new { Name = model.Name, Description = model.Description, ReleaseYear = model.ReleaseYear });
                }
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    result = connection.QuerySingle<MotionPicture>(query,
                        new { Id = model.Id, Name = model.Name, Description = model.Description, ReleaseYear = model.ReleaseYear });
                }
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
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var row = connection.Execute(query,
                        new { Id = id });
                }
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
