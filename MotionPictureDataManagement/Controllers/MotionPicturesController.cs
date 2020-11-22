using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly IDbConnection connection;

        public MotionPicturesController(ILogger<MotionPicturesController> logger,
            IDbConnection connection)
        {
            _logger = logger;
            this.connection = connection;
        }

        [HttpGet]
        public IEnumerable<MotionPicture> Get()
        {
            IEnumerable<MotionPicture> result;
            var query = "SELECT * FROM MotionPictures";
            try
            {
                result = connection.Query<MotionPicture>(query);
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
