export ASPNETCORE_ConnectionStrings__MainDb='Host=localhost\;Username=postgres\;Password=root\;Database=choresDb'
dotnet ef migrations add Init -o Data/Migrations -s '../chores.Web'
read -p "Press enter to continue"