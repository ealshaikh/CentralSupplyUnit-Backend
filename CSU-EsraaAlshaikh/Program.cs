using CSU_Core.Common;
using CSU_Core.Models;
using CSU_Core.Repository;
using CSU_Core.Service;
using CSU_Infra.Common;
using CSU_Infra.Repository;
using CSU_Infra.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(o =>
        {
            o.AddPolicy("policy", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        builder.Services.AddScoped<IDbContext, DbContext>();
        builder.Services.AddScoped<I_ItemService, ItemService>();
        builder.Services.AddScoped<I_ItemRepository, ItemRepository>();

        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IRoleService, RoleService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        builder.Services.AddScoped<IWarehouseService, WarehouseService>();

        builder.Services.AddScoped<ISupplyDocsRepository, SupplyDocumentRepository>();
        builder.Services.AddScoped<ISupplyDocsService, SupplyDocService>();

        builder.Services.AddScoped<I_LoginServicecs, LoginService>();
        builder.Services.AddScoped<I_LoginRepository, LoginRepository>();

        builder.Services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345superSecretKey@345"))
            };
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors("policy");
        app.Run();

    }
}

public class CustomSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(Item))
        {
            schema.Example = new OpenApiObject
            {
                ["itemid"] = new OpenApiInteger(0),
                ["itemname"] = new OpenApiString("string"),
                ["itemdescription"] = new OpenApiString("string"),
                ["quantity"] = new OpenApiInteger(0),
                ["warehouseid"] = new OpenApiInteger(0)
            };
        }

        if (context.Type == typeof(Role))
        {
            schema.Example = new OpenApiObject
            {
                ["roleid"] = new OpenApiInteger(0),
                ["name"] = new OpenApiString("string")
            };
        }

        if (context.Type == typeof(User))
        {
            schema.Example = new OpenApiObject
            {
                ["userid"] = new OpenApiInteger(0),
                ["firstname"] = new OpenApiString("string"),
                ["lastname"] = new OpenApiString("string"),
                ["roleid"] = new OpenApiInteger(0),
                ["email"] = new OpenApiString("string"),
                ["password"] = new OpenApiString("string")
            };
        }

        if (context.Type == typeof(Warehouse))
        {
            schema.Example = new OpenApiObject
            {
                ["warehouseid"] = new OpenApiInteger(0),
                ["warehousename"] = new OpenApiString("string"),
                ["warehousedescription"] = new OpenApiString("string"),
                ["createdby"] = new OpenApiInteger(0)
            };
        }

        if (context.Type == typeof(Supplydocument))
        {
            schema.Example = new OpenApiObject
            {
                ["documentid"] = new OpenApiInteger(0),
                ["documentname"] = new OpenApiString("string"),
                ["documentsubject"] = new OpenApiString("string"),
                ["createdby"] = new OpenApiInteger(0),
                ["warehouseid"] = new OpenApiInteger(0),
                ["itemid"] = new OpenApiInteger(0),
                ["status"] = new OpenApiString("string")
            };
        }

        if (context.Type == typeof(Login))
        {
            schema.Example = new OpenApiObject
            {
                ["email"] = new OpenApiString("string"),
                ["password"] = new OpenApiString("string")
            };
        }
    }
}
