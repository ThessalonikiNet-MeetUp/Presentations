using Common;
using Common.Services;
using Identity;
using Identity.Data;
using Identity.Models;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Register the IOptions object
builder.Services.Configure<ServicesConfiguration>(
	builder.Configuration.GetSection("Services"));
// Explicitly register the settings object by delegating to the IOptions object
builder.Services.AddSingleton(resolver =>
		resolver.GetRequiredService<IOptions<ServicesConfiguration>>().Value);
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options => 
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//https://docs.duendesoftware.com/identityserver/v6/quickstarts/5_aspnetid/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options => 
	{
		options.Events.RaiseErrorEvents = true;
		options.Events.RaiseInformationEvents = true;
		options.Events.RaiseFailureEvents = true;
		options.Events.RaiseSuccessEvents = true;

		// https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
		options.EmitStaticAudienceClaim = true;
		//options.UserInteraction.LoginUrl = ""
	})
	.AddInMemoryIdentityResources(IdentityConfig.IdentityResources)
	.AddInMemoryApiScopes(IdentityConfig.ApiScopes)
	.AddInMemoryClients(IdentityConfig.GetClients(builder.Configuration.GetRequiredSection("Services").Get<Common.ServicesConfiguration>().PublicWebApp))
	.AddAspNetIdentity<ApplicationUser>();

builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<ISmsService, SmsService>();


var app = builder.Build();

MigrateDatabase(app);

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();
app.MapRazorPages().RequireAuthorization();

app.Run();

static void MigrateDatabase(WebApplication app)
{
	using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
	{
		var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		context.Database.Migrate();
	}
}


