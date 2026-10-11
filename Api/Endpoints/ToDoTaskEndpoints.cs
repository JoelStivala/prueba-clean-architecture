using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ToDo.Application.Common.Dto;
using ToDo.Application.Services;
using ToDo.Application.Services.Implementations;
using ToDo.Infrastructure.Data.Entities;

namespace ToDo.Api.Endpoints
{
    public static class ToDoTaskEndpoints
    {
        public static WebApplication MapToDoTaskEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/to-do-tasks");

            group.MapPost("/", Create)
                .ProducesValidationProblem()
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            group.MapGet("/{id:int}", Get)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            group.MapPut("/{id:int}", Update)
                .ProducesValidationProblem()
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            group.MapDelete("/{id:int}", Delete)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            return app;
        }

        public static async Task<Created> Create(
            CreateToDoTaskDto createToDoTask,
            IToDoTaskService toDoTaskService
            )
        {
            await toDoTaskService.Create(createToDoTask);

            return TypedResults.Created();
        }

        public static async Task<Results<Ok<GetToDoTaskDto>, NotFound>> Get(int id, IToDoTaskService toDoTaskService)
        {
            var toDoTask = await toDoTaskService.Get(id);

            if(toDoTask == null)
                return TypedResults.NotFound();

            return TypedResults.Ok(toDoTask);
        }

        public static async Task<Results<NoContent, NotFound>> Update(int id,
            UpdateToDoTaskDto updateToDoTask,
            IToDoTaskService toDoTaskService
            )
        {
            var result = await toDoTaskService.Update(id, updateToDoTask);

            if (!result)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }

        public static async Task<Results<NoContent, NotFound>> Delete(
            int id,
            IToDoTaskService toDoTaskService
            )
        {
            var result = await toDoTaskService.Delete(id);

            if (!result)
                return TypedResults.NotFound();

            return TypedResults.NoContent();
        }
    }
}
