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

namespace MotionPictureDataManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotionPictureController : ControllerBase
    {
        string connectionString = "Server=localhost\\SQLExpress;Database=MotionPicture;Integrated Security=True;";
        private readonly ILogger<MotionPictureController> _logger;

        public MotionPictureController(ILogger<MotionPictureController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<MotionPicture> Get()
        {
            IEnumerable<MotionPicture> result;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    result = connection.Query<MotionPicture>("SELECT * FROM MotionPictures");
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                result = new List<MotionPicture>();
            }

            return result;
        }

        [HttpPost]
        [ProducesResponseType(typeof(MotionPicture), 200)]
        public IActionResult PostMotionPicture([FromBody] MotionPicture model)
        {
            MotionPicture result;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    result = connection.QuerySingle<MotionPicture>(
                        "INSERT INTO MotionPictures (Name, Description, ReleaseYear) OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.ReleaseYear, INSERTED.Description VALUES (@Name, @Description, @ReleaseYear)",
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

        [HttpPut]
        [ProducesResponseType(typeof(MotionPicture), 200)]
        public IActionResult PutMotionPicture([FromBody] MotionPicture model)
        {
            MotionPicture result;
            var query = "UPDATE MotionPictures SET Name = @Name, Description = @Description, ReleaseYear = @ReleaseYear "
                        + "OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.ReleaseYear, INSERTED.Description "
                        + "WHERE Id=@Id";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
    }
}
