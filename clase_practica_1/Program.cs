using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//se añade el servicio de la base de datos, como opciones dentro de su constructor utilizar la tabla task
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDataBase("Tasks"));
builder.Services.AddDatabaeDeveloperPageExceptionFilter();

//se construye la aplicacion
var app = builder.Build();

//se crea una url y dentro de esta ocupa mas url
var tasks = app.MapGroup("/task");

tasks.MapGet("/", GetAllTasks);
tasks.MapPost("/", CreateTasks);

app.Run();

//se crea un metodo estatico que se retorna una interfaz de ese resultado
static IResult GetAllTasks(DataContext db){

    return TypedResults.Ok(db.Tasks.ToList());

}

static IResult CreateTasks(Task task, DataContext db){

    db.Tasks.Add(Task);
    db.SaveChanges();

    return TypedResults.Ok();

}

//nota la fecha no soporta el ambito disk
