var builder = WebApplication.CreateBuilder(args);

//se añade el servicio de la base de datos, como opciones dentro de su constructor utilizar la tabla task
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDataBase("Tasks"));

//se construye la aplicacion
var app = builder.Build();

//se crea una url y dentro de esta ocupa mas url
var tasks = app.MapGroup("/task");

app.MapGet("/", () => "Hello World!");

app.Run();
