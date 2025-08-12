using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using YabaTalk.Api.Hubs;
using YabaTalk.Api.Realtime;
using YabaTalk.Application.Dtos;
using YabaTalk.Application.Interfaces;
using YabaTalk.Application.Interfaces.Repository;
using YabaTalk.Application.Interfaces.Service;
using YabaTalk.Infrastructure.Consumer;
using YabaTalk.Infrastructure.Database;
using YabaTalk.Infrastructure.Repository;
using YabaTalk.Infrastructure.Services.Auth;
using YabaTalk.Infrastructure.Services.Message;

var builder = WebApplication.CreateBuilder(args);

// Db
builder.Services.AddDbContext<YabaDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// MassTransit
builder.Services.Configure<MassTransitSettings>(builder.Configuration.GetSection("MassTransitSettings"));
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateMessageDtoConsumer>();
    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        var s = context.GetRequiredService<IOptions<MassTransitSettings>>().Value;

        cfg.Host(s.Host, s.VirtualHost, h =>
        {
            h.Username(s.Username);
            h.Password(s.Password);
        });

        cfg.ReceiveEndpoint("message-service", e =>
        {
            e.ConfigureConsumer<CreateMessageDtoConsumer>(context);
        });
    });
});

// MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "YabaTalk API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT token'ı 'Bearer {token}' şeklinde girin."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() }
    });
});

// DI
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<TokenProvider>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IActiveUserAccessor, ActiveUserAccessor>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>();

// Auth
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

// SignalR
builder.Services.AddSignalR();
builder.Services.AddScoped<IRealtimeNotifier, SignalRRealtimeNotifier>();

builder.Services.AddCors(p => p.AddDefaultPolicy(b =>
    b.SetIsOriginAllowed(_ => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapSwagger().AllowAnonymous();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "YabaTalk API v1"); c.RoutePrefix = "swagger"; });
}

app.UseHttpsRedirection();

app.UseCors();             // gerekiyorsa
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/hubs/chat");

app.Run();
