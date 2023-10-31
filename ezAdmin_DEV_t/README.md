Cài đặt Microsoft.EntityFrameworkCore.Tools để thực hiện các tác vụ cho Entity Framework (EF) Core  tại thời điểm thiết kế. Ví dụ: sinh các class trong model tương ứng với các bảng trong CSDL. 
1. Vào Tools >> Manage NuGet Package
2. Search gói Microsoft.EntityFrameworkCore.Tools và cài đặt
3. Các lệnh có thể được thực hiện bao gồm: 

Add-Migration
Bundle-Migration
Drop-Database
Get-DbContext
Get-Migration
Optimize-DbContext
Remove-Migration
Scaffold-DbContext
Script-Migration
Update-Database -Args '--environment Production'


Ví dụ: Tạo các EF từ DB: 

For windows:
Scaffold-DbContext "Server=.\SQLEXPRESS;Database=ezAdmin;Trusted_Connection=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir EntityModels -ContextDir DBContext -Context ezSQLDBContext -NoOnConfiguring -DataAnnotations -f -t AspNetGroupUser, AspNetRoleClaims, AspNetRoles, AspNetUserClaims, AspNetUserLogins, AspNetUserRoles, AspNetUsers, AspNetUserSessions, AspNetUserSites, AspNetUserTokens, Companies, Countries, Keywords,LanguageKeywords, Languages, Logs, MenuDefine, MenuDefineAspNetGroupUsers

For MacOS:
1. Cài dotnet-ef theo lệnh
dotnet tool install --global dotnet-ef

2. Kiểm tra xem đã cài ok chưa? 
dotnet ef

3. Thực thi
- Click phải vào thư mục Models, chọn Open Terminal. 
- Chạy câu lệnh sau:
dotnet ef dbcontext scaffold "Data Source=localhost,1433;Initial Catalog=ezAdmin_DEV;User ID=sa;Password=Dung1708@;encrypt=true;trustServerCertificate=true;" Microsoft.EntityFrameworkCore.SqlServer --output-dir EntityModels --context-dir DBContext --context ezSQLDBContext --no-onconfiguring --data-annotations --force -t AspNetGroupUser -t AspNetRoleClaims -t AspNetRoles -t AspNetUserClaims -t AspNetUserLogins -t AspNetUserRoles -t AspNetUsers -t AspNetUserSessions -t AspNetUserSites -t AspNetUserTokens -t Companies -t Countries -t Keywords -t LanguageKeywords -t Languages -t Logs -t MenuDefine -t MenuDefineAspNetGroupUsers


PROD
{
  "ezIDToken": "1EDC03ECD1C72FE71882FB9555E5A9B69766E7ED79B0A4D7B33378F0E35048A3"
}

DEV:
ezIDAccessToken: "0945C0FFC9CA0201EA9731AB3226DC25A47B061AED80A7D4850291F842D66FD5"

Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySUQiOiI5Yzk2OWU1Yi03MDIxLTQzOGMtOWUzMy00NDM4ZTVjODFjNjkiLCJuYmYiOjE2OTY1NTk0NDMsImV4cCI6MTY5OTE1MTQ0M30.W1JVuItRK8ezHaS_09NxYK8kZrJko-b2LXCl5J5NHak



