namespace GestaoDeEquipamento.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(); 
            
            var app = builder.Build();

            app.UseRouting();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}
