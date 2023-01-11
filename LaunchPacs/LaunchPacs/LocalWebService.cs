namespace LaunchPacs
{
    public class LocalWebService 
    {
        public WebApplication App { get; set; }
        public LocalWebService()
        {
            StartKestrel();
        }

        public void StartKestrel()
        {
            string[] args = new List<string>().ToArray();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

        }
        public void Start()
        {
            App.Run();
        }

        public void Stop()
        {
            App.StopAsync().Wait();
        }
    }
}
