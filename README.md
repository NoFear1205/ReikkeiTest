# ReikkeiTest
Project được xây dựng với kiến trúc Clean Architecture pattern. Clean Architecture là một kiến trúc phần mềm giúp tách biệt các lớp và module trong một ứng dụng, tạo ra một cấu trúc rõ ràng và dễ bảo trì.

## Cấu trúc thư mục

- `src/`: Thư mục chứa mã nguồn của ứng dụng.
  - `Application/`: Chứa các use cases và logic ứng dụng.
  - `Domain/`: Định nghĩa các đối tượng cốt lõi của ứng dụng.
  - `Infrastructure/`: Chứa các đối tượng liên quan đến việc triển khai, như các lớp lưu trữ và giao diện người dùng.
  - `API/`: Định nghĩa các thành phần liên quan đến giao diện người dùng, như các controller và view models.

## Công nghệ sử dụng

- Ngôn ngữ lập trình: C#
- Framework: .NET Core
- Cơ sở dữ liệu: Sql Server

## Hướng dẫn sử dụng

1. Clone repository về máy local.
2. Mở solution trong IDE (Visual Studio, Visual Studio Code, Rider, ...).
3. Sử dụng file db.bac để import database vào sql server
4. Build và chạy ứng dụng.
![image](https://github.com/NoFear1205/ReikkeiTest/assets/87076351/1dbd629d-22f0-4c0f-a4b1-6005216ec2d7)
