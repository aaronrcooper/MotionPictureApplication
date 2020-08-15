using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotionPictureDataManagement.API.Models;
using MotionPictureDataManagement.Controllers;

namespace MotionPictureDataManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionPicturesController : ControllerBase
    {
        private readonly ILogger<MotionPicturesController> _logger;
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public MotionPicturesController(ILogger<MotionPicturesController> logger,
            IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _connectionString = _config.GetConnectionString("MotionPictureDb");
        }

        [HttpGet]
        public IEnumerable<MotionPicture> Get()
        {
            IEnumerable<MotionPicture> result;
            var query = "SELECT * FROM MotionPictures";
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    result = connection.Query<MotionPicture>(query);
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                result = new List<MotionPicture>();
            }

            return result;
        }
    }
}
