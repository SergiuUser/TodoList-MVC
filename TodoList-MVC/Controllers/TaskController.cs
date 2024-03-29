﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoList_MVC.Data;
using Todo_List_WebApp.Models;
using TodoList_MVC.Models.DTOs;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using TodoList_MVC.Services;

namespace TodoList_MVC.Controllers
{
    public class TaskController : Controller
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        private readonly ITaskService _service;

        public TaskController(TodoContext context, IMapper mapper, ITaskService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            int ID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var todoContext = await _context.Tasks.Where(t => t.UserID == ID).ToListAsync();
            return View(todoContext);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskModel = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            return View(taskModel);
        }

        public IActionResult Create()
        {
            ViewBag.Priorities = _service.GetPriorities();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,Priority")] TaskDTO entity)
        {
            if (ModelState.IsValid)
            {

                var entityMapped = _mapper.Map<TaskModel>(entity);

                entityMapped.UserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));


                _context.Add(entityMapped);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Tasks.FindAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            ViewBag.Priorities = _service.GetPriorities();
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,Priority")] TaskDTO entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var entityMapped = _mapper.Map<TaskModel>(entity);

                    entityMapped.UserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    _context.Update(entityMapped);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskModelExists(entity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskModel = await _context.Tasks.FindAsync(id);
            if (taskModel != null)
            {
                _context.Tasks.Remove(taskModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskModelExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }

        [HttpPatch("api/tasks/markComplete/{id}")]
        [Authorize]
        public async Task<IActionResult> MarkComplete(int id)
        {
            var taskToUpdate = await _context.Tasks.FindAsync(id);
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            if (taskToUpdate.UserID != Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return Unauthorized();
            }

            if (taskToUpdate.isCompleted == false)
            taskToUpdate.isCompleted = true;
            else taskToUpdate.isCompleted = false;  

            _context.Update(taskToUpdate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
