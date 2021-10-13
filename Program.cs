using MiniTodo.Data;
using MiniTodo.ViewModels;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("v1/todos", (AppDbContext context) => 
{
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
});

app.MapPost("v1/todos", (AppDbContext context, CreateTodoViewModel model) => 
{
    var todo = model.MapTo();

    if(model.IsValid == false)
        return Results.BadRequest(model.Notifications);

    context.Todos.Add(todo);
    context.SaveChanges();

    return Results.Created($"/v1/todos/{todo.Id}", todo);
});

app.Run();