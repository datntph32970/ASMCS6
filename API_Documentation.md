# Tài liệu API - Hệ thống Quản lý Đơn hàng

## Tổng quan

Hệ thống API được xây dựng bằng ASP.NET Core với JWT Authentication, hỗ trợ quản lý đơn hàng, sản phẩm, người dùng và các chức năng khác.

### Base URL

```
http://localhost:7294/api
```

### Authentication

Hệ thống sử dụng JWT Bearer Token. Thêm header sau vào các request cần xác thực:

```
Authorization: Bearer {your_jwt_token}
```

### Response Format

Tất cả API đều trả về response theo format:

```json
{
  "success": true/false,
  "message": "Thông báo",
  "data": {...},
  "errors": [...]
}
```

## 1. Authentication APIs

### 1.1 Đăng nhập

**POST** `/api/Auth/login`

**Request Body:**

```json
{
  "username": "string (required, 10-50 chars)",
  "password": "string (required, 6-50 chars)"
}
```

**Response:**

```json
{
  "success": true,
  "message": "Đăng nhập thành công",
  "data": {
    "token": "jwt_token_string",
    "user": {
      "id": "guid",
      "username": "string",
      "fullName": "string",
      "email": "string",
      "phone": "string",
      "address": "string",
      "roleId": "guid",
      "role": {
        "id": "guid",
        "roleName": "string"
      }
    }
  }
}
```

### 1.2 Đăng ký

**POST** `/api/Auth/register`

**Request Body:**

```json
{
  "username": "string (required, 10-50 chars)",
  "password": "string (required, 6-50 chars)",
  "confirmPassword": "string (required, must match password)",
  "fullName": "string (required)",
  "email": "string (email format)",
  "phone": "string (10 digits)",
  "address": "string"
}
```

**Response:**

```json
{
  "success": true,
  "message": "Đăng ký thành công",
  "data": {
    "token": "jwt_token_string",
    "user": {...}
  }
}
```

### 1.3 Xác thực người dùng

**GET** `/api/Auth/validate?username={username}&password={password}`

**Response:**

```json
true/false
```

## 2. Users APIs

### 2.1 Lấy danh sách người dùng

**GET** `/api/Users?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

**Quyền:** Admin

**Query Parameters:**

- `pageIndex`: Số trang (default: 1)
- `pageSize`: Số item mỗi trang (default: 20)
- `sortColumn`: Cột sắp xếp (default: "createdDate")
- `sortOrder`: Thứ tự sắp xếp "asc"/"desc" (default: "desc")

**Response:**

```json
{
  "success": true,
  "message": "Lấy danh sách thành công",
  "data": {
    "items": [
      {
        "id": "guid",
        "username": "string",
        "fullName": "string",
        "email": "string",
        "phone": "string",
        "address": "string",
        "roleId": "guid",
        "role": {
          "id": "guid",
          "roleName": "string"
        },
        "createdDate": "datetime",
        "createdByName": "string"
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 100,
    "totalPage": 5
  }
}
```

### 2.2 Lấy thông tin người dùng

**GET** `/api/Users/{id}`

**Response:**

```json
{
  "success": true,
  "message": "Lấy thông tin người dùng thành công",
  "data": {
    "id": "guid",
    "username": "string",
    "fullName": "string",
    "email": "string",
    "phone": "string",
    "address": "string",
    "roleId": "guid",
    "role": {...}
  }
}
```

### 2.3 Tạo người dùng mới

**POST** `/api/Users`

**Quyền:** Admin

**Request Body:**

```json
{ 
  "username": "string (required, 10-50 chars)",
  "password": "string (required, 6-50 chars)",
  "fullName": "string (required)",
  "email": "string (email format)",
  "phone": "string (10 digits)",
  "address": "string",
  "roleId": "guid (required)"
}
```

### 2.4 Cập nhật người dùng

**PUT** `/api/Users`

**Request Body:**

```json
{
  "id": "guid (required)",
  "username": "string",
  "fullName": "string",
  "email": "string",
  "phone": "string",
  "address": "string",
  "roleId": "guid"
}
```

### 2.5 Xóa người dùng

**DELETE** `/api/Users/{id}`

**Quyền:** Admin

## 3. Products APIs

### 3.1 Lấy danh sách sản phẩm

**GET** `/api/Products?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

**Response:**

```json
{
  "success": true,
  "message": "Lấy danh sách sản phẩm thành công",
  "data": {
    "items": [
      {
        "id": "guid",
        "productName": "string",
        "description": "string",
        "price": "decimal",
        "imageURL": "string",
        "categoryID": "guid",
        "category": {
          "id": "guid",
          "categoryName": "string"
        },
        "createdDate": "datetime"
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 100,
    "totalPage": 5
  }
}
```

### 3.2 Lấy thông tin sản phẩm

**GET** `/api/Products/{id}`

### 3.3 Tạo sản phẩm mới

**POST** `/api/Products`

**Quyền:** Admin, Staff

**Request Body (FormData):**

```
productName: string (required)
description: string
price: decimal (required)
image: file (optional)
categoryID: guid (required)
```

### 3.4 Cập nhật sản phẩm

**PUT** `/api/Products`

**Quyền:** Admin, Staff

**Request Body (FormData):**

```
id: guid (required)
productName: string
description: string
price: decimal
image: file (optional)
categoryID: guid
```

### 3.5 Xóa sản phẩm

**DELETE** `/api/Products/{id}`

**Quyền:** Admin

## 4. Categories APIs

### 4.1 Lấy danh sách danh mục

**GET** `/api/Categories?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

**Response:**

```json
{
  "success": true,
  "message": "Lấy danh sách danh mục thành công",
  "data": {
    "items": [
      {
        "id": "guid",
        "categoryName": "string",
        "description": "string",
        "createdDate": "datetime"
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 50,
    "totalPage": 3
  }
}
```

### 4.2 Lấy thông tin danh mục

**GET** `/api/Categories/{id}`

### 4.3 Tạo danh mục mới

**POST** `/api/Categories`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "categoryName": "string (required)",
  "description": "string"
}
```

### 4.4 Cập nhật danh mục

**PUT** `/api/Categories`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "id": "guid (required)",
  "categoryName": "string",
  "description": "string"
}
```

### 4.5 Xóa danh mục

**DELETE** `/api/Categories/{id}`

**Quyền:** Admin

## 5. Orders APIs

### 5.1 Lấy danh sách đơn hàng

**GET** `/api/Orders?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

**Response:**

```json
{
  "success": true,
  "message": "Lấy danh sách đơn hàng thành công",
  "data": {
    "items": [
      {
        "id": "guid",
        "customerID": "guid",
        "staffID": "guid",
        "orderDate": "datetime",
        "totalAmount": "decimal",
        "customer": {
          "id": "guid",
          "fullName": "string"
        },
        "staff": {
          "id": "guid",
          "fullName": "string"
        },
        "orderDetails": [...],
        "statusOrders": [...],
        "createdDate": "datetime"
      }
    ],
    "pageIndex": 1,
    "pageSize": 20,
    "totalCount": 100,
    "totalPage": 5
  }
}
```

### 5.2 Lấy thông tin đơn hàng

**GET** `/api/Orders/{id}`

### 5.3 Tạo đơn hàng mới

**POST** `/api/Orders`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "customerID": "guid (required)",
  "totalAmount": "decimal (required)"
}
```

### 5.4 Cập nhật đơn hàng

**PUT** `/api/Orders`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "id": "guid (required)",
  "customerID": "guid",
  "staffID": "guid",
  "orderDate": "datetime",
  "totalAmount": "decimal"
}
```

### 5.5 Xóa đơn hàng

**DELETE** `/api/Orders/{id}`

**Quyền:** Admin

## 6. Combos APIs

### 6.1 Lấy danh sách combo

**GET** `/api/Combos?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

### 6.2 Lấy thông tin combo

**GET** `/api/Combos/{id}`

### 6.3 Tạo combo mới

**POST** `/api/Combos`

**Quyền:** Admin, Staff

**Request Body (FormData):**

```
comboName: string (required)
description: string
price: decimal (required)
image: file (optional)
```

### 6.4 Cập nhật combo

**PUT** `/api/Combos`

**Quyền:** Admin, Staff

**Request Body (FormData):**

```
id: guid (required)
comboName: string
description: string
price: decimal
image: file (optional)
```

### 6.5 Xóa combo

**DELETE** `/api/Combos/{id}`

**Quyền:** Admin

## 7. OrderDetails APIs

### 7.1 Lấy danh sách chi tiết đơn hàng

**GET** `/api/OrderDetails?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

### 7.2 Lấy thông tin chi tiết đơn hàng

**GET** `/api/OrderDetails/{id}`

### 7.3 Tạo chi tiết đơn hàng mới

**POST** `/api/OrderDetails`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "orderID": "guid (required)",
  "productID": "guid (required)",
  "quantity": "int (required)",
  "price": "decimal (required)"
}
```

### 7.4 Cập nhật chi tiết đơn hàng

**PUT** `/api/OrderDetails`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "id": "guid (required)",
  "orderID": "guid",
  "productID": "guid",
  "quantity": "int",
  "price": "decimal"
}
```

### 7.5 Xóa chi tiết đơn hàng

**DELETE** `/api/OrderDetails/{id}`

**Quyền:** Admin

## 8. ComboDetails APIs

### 8.1 Lấy danh sách chi tiết combo

**GET** `/api/ComboDetails?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

### 8.2 Lấy thông tin chi tiết combo

**GET** `/api/ComboDetails/{id}`

### 8.3 Tạo chi tiết combo mới

**POST** `/api/ComboDetails`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "comboID": "guid (required)",
  "productID": "guid (required)",
  "quantity": "int (required)"
}
```

### 8.4 Cập nhật chi tiết combo

**PUT** `/api/ComboDetails`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "id": "guid (required)",
  "comboID": "guid",
  "productID": "guid",
  "quantity": "int"
}
```

### 8.5 Xóa chi tiết combo

**DELETE** `/api/ComboDetails/{id}`

**Quyền:** Admin

## 9. Status APIs

### 9.1 Lấy danh sách trạng thái

**GET** `/api/Status?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

### 9.2 Lấy thông tin trạng thái

**GET** `/api/Status/{id}`

### 9.3 Tạo trạng thái mới

**POST** `/api/Status`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "statusName": "string (required)",
  "description": "string"
}
```

### 9.4 Cập nhật trạng thái

**PUT** `/api/Status`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "id": "guid (required)",
  "statusName": "string",
  "description": "string"
}
```

### 9.5 Xóa trạng thái

**DELETE** `/api/Status/{id}`

**Quyền:** Admin

## 10. StatusOrders APIs

### 10.1 Lấy danh sách trạng thái đơn hàng

**GET** `/api/StatusOrders?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

### 10.2 Lấy thông tin trạng thái đơn hàng

**GET** `/api/StatusOrders/{id}`

### 10.3 Tạo trạng thái đơn hàng mới

**POST** `/api/StatusOrders`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "orderID": "guid (required)",
  "statusID": "guid (required)",
  "note": "string"
}
```

### 10.4 Cập nhật trạng thái đơn hàng

**PUT** `/api/StatusOrders`

**Quyền:** Admin, Staff

**Request Body:**

```json
{
  "id": "guid (required)",
  "orderID": "guid",
  "statusID": "guid",
  "note": "string"
}
```

### 10.5 Xóa trạng thái đơn hàng

**DELETE** `/api/StatusOrders/{id}`

**Quyền:** Admin

## 11. Roles APIs

### 11.1 Lấy danh sách vai trò

**GET** `/api/Roles?pageIndex={page}&pageSize={size}&sortColumn={column}&sortOrder={order}`

### 11.2 Lấy thông tin vai trò

**GET** `/api/Roles/{id}`

### 11.3 Tạo vai trò mới

**POST** `/api/Roles`

**Quyền:** Admin

**Request Body:**

```json
{
  "roleName": "string (required)",
  "description": "string"
}
```

### 11.4 Cập nhật vai trò

**PUT** `/api/Roles`

**Quyền:** Admin

**Request Body:**

```json
{
  "id": "guid (required)",
  "roleName": "string",
  "description": "string"
}
```

### 11.5 Xóa vai trò

**DELETE** `/api/Roles/{id}`

**Quyền:** Admin

## Cấu trúc Database

### Các Entity chính:

1. **Users** - Người dùng

   - id (Guid, PK)
   - username (string, required, 10-50 chars)
   - password (string, required, 6-50 chars)
   - fullName (string, required)
   - email (string, email format)
   - phone (string, 10 digits)
   - address (string)
   - roleId (Guid, FK to Roles)

2. **Products** - Sản phẩm

   - id (Guid, PK)
   - productName (string, required)
   - description (string)
   - price (decimal, required)
   - imageURL (string)
   - categoryID (Guid, FK to Categories)

3. **Categories** - Danh mục

   - id (Guid, PK)
   - categoryName (string, required)
   - description (string)

4. **Orders** - Đơn hàng

   - id (Guid, PK)
   - customerID (Guid, FK to Users)
   - staffID (Guid, FK to Users, nullable)
   - orderDate (DateTime)
   - totalAmount (decimal)

5. **OrderDetails** - Chi tiết đơn hàng

   - id (Guid, PK)
   - orderID (Guid, FK to Orders)
   - productID (Guid, FK to Products)
   - quantity (int)
   - price (decimal)

6. **Combos** - Combo sản phẩm

   - id (Guid, PK)
   - comboName (string, required)
   - description (string)
   - price (decimal, required)
   - imageURL (string)

7. **ComboDetails** - Chi tiết combo

   - id (Guid, PK)
   - comboID (Guid, FK to Combos)
   - productID (Guid, FK to Products)
   - quantity (int)

8. **Status** - Trạng thái

   - id (Guid, PK)
   - statusName (string, required)
   - description (string)

9. **StatusOrders** - Trạng thái đơn hàng

   - id (Guid, PK)
   - orderID (Guid, FK to Orders)
   - statusID (Guid, FK to Status)
   - note (string)

10. **Roles** - Vai trò
    - id (Guid, PK)
    - roleName (string, required)
    - description (string)

## Lưu ý quan trọng

1. **Authentication**: Hầu hết API đều yêu cầu JWT token, trừ các API Auth
2. **Authorization**: Sử dụng role-based access control với các role: Admin, Staff, User
3. **File Upload**: Các API tạo/cập nhật Products và Combos hỗ trợ upload hình ảnh qua FormData
4. **Pagination**: Tất cả API GET danh sách đều hỗ trợ phân trang
5. **Error Handling**: Tất cả API đều trả về response format thống nhất với thông báo lỗi chi tiết
6. **Validation**: Các field có validation rules được định nghĩa trong ViewModels

## Các HTTP Status Codes

- **200 OK**: Thành công
- **201 Created**: Tạo mới thành công
- **400 Bad Request**: Dữ liệu không hợp lệ
- **401 Unauthorized**: Chưa xác thực
- **403 Forbidden**: Không có quyền truy cập
- **404 Not Found**: Không tìm thấy resource
- **500 Internal Server Error**: Lỗi server
