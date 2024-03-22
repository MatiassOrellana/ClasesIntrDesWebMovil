using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

//se añade el servicio de la base de datos, como opciones dentro de su constructor utilizar la tabla task
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Tasks"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//se construye la aplicacion
var app = builder.Build();

//se crea una url y dentro de esta ocupa mas url
var tasks = app.MapGroup("/task");

//Metodos HTTP, generalmente se ocupan en los controladores
tasks.MapGet("/", GetAllTasks);
tasks.MapGet("/complete", GetCompleteTasks);
tasks.MapGet("/{id}", GetTask);
tasks.MapPost("/", CreateTasks);
tasks.MapPut("/{id}", UpdateTask);
tasks.MapDelete("/{id}", DeleteTask);

app.Run();

//se crea un metodo estatico que se retorna una interfaz de ese resultado
static IResult GetAllTasks(DataContext db){

    return TypedResults.Ok(db.Tasks.ToArray());

}

//recurso que obtiene todas las tareas que han sido completadas
static IResult GetCompleteTasks(DataContext db){

    return TypedResults.Ok(db.Tasks.Where(t => t.IsComplete).ToList());

}

//recurso que obtiene alguna tarea en especifica por ID
static IResult GetTask(int id, DataContext db){

    var task = db.Tasks.Find(id);

    if(task is null) return TypedResults.NotFound();

    return TypedResults.Ok(task);

}

//recurso que permite crear alguna tarea
static IResult CreateTasks(Task task, DataContext db){

    db.Tasks.Add(task);
    db.SaveChanges();

    return TypedResults.Created($"/tasks/{task.Id}", task);

}

//recurso que actualiza la tarea
static IResult UpdateTask(int id, Task inputTask, DataContext db){

    var task = db.Tasks.Find(id);

    if(task is null) return TypedResults.NotFound();

    task.Name = inputTask.Name;
    task.IsComplete = inputTask.IsComplete;

    db.SaveChangesAsync();

    return TypedResults.Ok();
}

static IResult DeleteTask(int id, DataContext db){

    var task = db.Tasks.Find(id);

    if(task is null) return TypedResults.NotFound();

    db.Tasks.Remove(task);
    db.SaveChangesAsync();
    return TypedResults.Ok();


}

//nota la fecha no soporta el ambito disk
