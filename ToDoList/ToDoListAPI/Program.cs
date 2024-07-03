using GraphQL;
using GraphQL.Types;
using ToDoList.Factory;
using ToDoList.Models;
using ToDoList.Repository;
using ToDoListAPI.Mutation;
using ToDoListAPI.Query;
using ToDoListAPI.Schema;
using ToDoListAPI.TypeObject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<DapperRepository<Tasks>>();
builder.Services.AddTransient<IRepository<Tasks>, DapperRepository<Tasks>>();
builder.Services.AddTransient<DapperRepository<Category>>();
builder.Services.AddTransient<IRepository<Category>, DapperRepository<Category>>();

builder.Services.AddTransient<SqlRepositoryFactory>();
builder.Services.AddTransient<IRepositoryFactory, SqlRepositoryFactory>();

builder.Services.AddTransient<TaskType>();
builder.Services.AddTransient<CategoryType>();
builder.Services.AddTransient<TaskInputType>();
builder.Services.AddTransient<CategoryInputType>();

builder.Services.AddTransient<TaskMutation>();
builder.Services.AddTransient<CategoryMutation>();

builder.Services.AddSingleton<RootQuery>();
builder.Services.AddSingleton<RootMutation>();

builder.Services.AddSingleton<ISchema, RootSchema>();

builder.Services.AddGraphQL(options =>
{
    options.AddAutoSchema<ISchema>().AddSystemTextJson();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGraphQLAltair("/graphql");

app.UseGraphQL<ISchema>();

app.UseHttpsRedirection();

app.Run();
