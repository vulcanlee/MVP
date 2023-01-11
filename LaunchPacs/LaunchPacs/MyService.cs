using NLog.Web;

namespace LaunchPacs
{
    public class MyService 
    {
        public WebApplication App { get; set; }
        public void Start()
        {
            string[] args = new List<string>().ToArray();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseNLog();

            App = builder.Build();

            // Configure the HTTP request pipeline.
            if (App.Environment.IsDevelopment())
            {
                App.UseSwagger();
                App.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            App.UseAuthorization();


            App.MapControllers();

            App.Start();
        }

        public void Stop()
        {
            App.StopAsync().Wait();
        }
    }
}
