﻿using CitWebApi.DB;
using CitWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitWebApi.Controllers;

[ApiController]
[Route("/stats")]
public class StatsController : ControllerBase
{
    private readonly ApiContext Db;
    private readonly IAccessTokenValidator Validator;

    public StatsController(ApiContext db, IAccessTokenValidator validator)
    {
        Db = db;
        Validator = validator;
    }
    
    [HttpGet]
    public IActionResult GetStats(string accessToken)
    {
        if (Validator.Validate(accessToken))
        {
            return BadRequest(new
            {
                Message = "Access token invalid."
            });
        }
        var statsData = new
        {
            TotalEntries = Db.StudentEntries.Count(x => x.Date.Date == DateTime.Now.Date),
            TotalStaffs = Db.Staffs.Count(),
            TotalIns = Db.StudentEntries.Count(x => x.Date.Date == DateTime.Now.Date),
            TotalOuts = Db.StudentEntries.Count(x => x.Date.Date == DateTime.Now.Date &&
                                                     x.OutTime != null),
            TotalStudents = Db.Students.Count(),
            AbsentToday = Db.Students
                .Where(x => 
                    !Db.StudentAttendances.Any(
                        y => y.Student.CardId == x.CardId &&
                             y.Date.Date == DateTime.Now.Date)).ToList().Count,
            TotalOdStudents = Db.StudentAttendances.Where(x => x.Date.Date == DateTime.Now.Date &&
                                                               x.isOnDuty == true).ToList().Count
        };
        return Ok(new
        {
            Data = statsData
        });
    }
}