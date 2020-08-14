﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsPortal.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;

        public EventController(IEventService eventService, IUserService userService)
        {
            _eventService = eventService;
            _userService = userService;
        }

        //[HttpGet]
        //public async Task<IEnumerable<int>> GetAllowedEventToVisitList()
        //{
        //    return await _eventService.IsEventUserCreated(User.Identity.Name);
        //}

        [HttpGet]
        public IEnumerable<EventDTO> GetPublicEvents()
        {
            return _eventService.GetPublicEvents();
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<EventDTO> GetPublicOwnEvents()
        {
            return _eventService.GetPublicOwnEvents(User.Identity.Name);
        }

        [Authorize]
        [HttpGet("{eventName}")]
        public IEnumerable<EventDTO> SearchEvents(string eventName)
        {
            return _eventService.SearchEvents(User.Identity.Name, eventName);
        }

        [Authorize]
        [HttpGet("{id}")]
        public EventDTO GetEvent(int? id)
        {
            return _eventService.GetEvent(id);
        }

        [Authorize]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<EventDTO>> CreateEvent([FromBody] EventDTO eventDTO)
        {
            try
            {
                if (eventDTO != null)
                {
                    eventDTO.OrganizerId = _userService.FindUserByEmail(User.Identity.Name).Id;
                    await _eventService.AddEvent(eventDTO);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(int? id)
        {
            try
            {
                await _eventService.DeleteEvent(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDTO eventDTO)
        {
            try
            {
                await _eventService.EditEvent(eventDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        //catch (DBConcurrencyException)
        //{
        //    if (_eventService.GetEvent(id) == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        throw;
        //    }
    }
}

