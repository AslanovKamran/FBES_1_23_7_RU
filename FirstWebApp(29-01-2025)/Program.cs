var builder = WebApplication.CreateBuilder(args);

//"Учим" наше приложение использовать Controllers и Views
//Через builder.Services в наш проект добавляются зависимости напр связь с БД 
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Добавляем middleware, который будет уметь работать со статическими файлами
//Напр. фотографии, стили, favicon и тп.
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

else
{
    app.UseStatusCodePagesWithRedirects("/Home/Error?code={0}");
    app.UseExceptionHandler("/Home/Error");
}


//Задаем нашему приложению путь по умолчанию (при необходимости его можно менять)
//Таким образом при открытии браузера в URL будет /Home/Index
//Где Home - имя контроллера (раздела майта), a Index - имя метода (страницы)
app.MapDefaultControllerRoute();


app.Run();
