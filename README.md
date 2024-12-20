1. Copy truy vấn từ StoreManagement.sql bên trong folder Data.
2. Dán truy vấn vào ssms tại new query.
3. Cơ sở dữ liệu có tên là StoreManagementProject
4. Vào Visual Studio chọn Tools/Connect to database
5. Vào SSMS có cửa sổ kết nối hiện ra, copy phần server name rồi dán vào phần server name trong bước trên
6. Click vào ô trust server certificate
7. Chọn database StoreManagementProject
8. Click test connection để xem kết nối đã thành công chưa
9. Nếu đã thành công, chọn advanced rồi copy connection string ở cuối cùng
10. Dán connection string vào biến connectionString trong Helpers/DatabaseConnection.cs 

Lưu ý: nếu connectionString bị lỗi, hãy dán tên server của sql server vào connectionString thay thế cho tên server ở trong source code.
Tên đăng nhập mặc định truy cập admin là admin và mật khẩu là admin.
