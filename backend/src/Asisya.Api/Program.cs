using Microsoft.AspNetCore.Builder;
var b=WebApplication.CreateBuilder(args);
var app=b.Build();
app.MapGet("/",()=> "API RUNNING");
app.Run();