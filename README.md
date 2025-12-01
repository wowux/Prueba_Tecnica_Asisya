# Asisya - DEV II (Prueba técnica)

## Arquitectura

### Backend (.NET 7)
- **Clean Architecture**: Domain / Application / Infrastructure / Api
- **Autenticación**: JWT Bearer tokens
- **Base de datos**: SQL Server con Entity Framework Core
- **Bulk Operations**: EFCore.BulkExtensions para cargas masivas
- **Testing**: xUnit con Moq y TestContainers

### Frontend (React + TypeScript)
- **SPA**: Single Page Application con React Router
- **Auth Guard**: Protección de rutas con JWT
- **HTTP Client**: Axios con interceptores automáticos
- **State Management**: React hooks locales

## Requisitos
- Docker & Docker Compose
- .NET 7 SDK
- Node 18+
- SQL Server (incluido en docker-compose)

## Ejecución

### Con Docker (Recomendado)
```bash
docker-compose up --build
```

### Desarrollo local
```bash
# Backend
cd backend
dotnet restore
dotnet run --project src/Asisya.Api

# Frontend
cd frontend
npm install
npm start
```

## URLs
- **API**: http://localhost:5000
- **Frontend**: http://localhost:3000
- **Swagger**: http://localhost:5000/swagger

## Endpoints principales

### Autenticación
- `POST /api/Auth/login` → `{ username: "admin", password: "password" }`

### Categorías
- `POST /api/Category` (Auth required) → Crear categoría
- `GET /api/Category` → Listar categorías

### Productos
- `POST /api/Product?count=100000` (Auth required) → Generar N productos
- `GET /api/Product?page=1&size=20&search=term` → Listar con paginación
- `GET /api/Product/{id}` → Detalle de producto
- `PUT /api/Product/{id}` (Auth required) → Actualizar
- `DELETE /api/Product/{id}` (Auth required) → Eliminar

## Escalabilidad

### Estrategias implementadas:
1. **Bulk Insert**: EFCore.BulkExtensions para cargas masivas
2. **Paginación**: Consultas eficientes con Skip/Take
3. **Índices**: Configurados en Name, Sku, CategoryId
4. **DTOs**: Mapeo explícito sin exponer entidades

### Escalabilidad horizontal (Cloud):
1. **API**: Múltiples instancias detrás de Load Balancer
2. **Base de datos**: Read replicas, particionamiento
3. **Cache**: Redis para consultas frecuentes
4. **CDN**: Para assets estáticos del frontend
5. **Message Queues**: Para procesamiento asíncrono
6. **Container Orchestration**: Kubernetes para auto-scaling

## Seguridad
- JWT con expiración de 24 horas
- CORS configurado para frontend
- Validación de entrada en DTOs
- Autorización en endpoints críticos
- HTTPS en producción

## Testing
```bash
cd backend
dotnet test Asisya.Tests/
```

- **Unit Tests**: Repository pattern con InMemory DB
- **Integration Tests**: WebApplicationFactory
- **Mocking**: Moq para dependencias

## CI/CD
- **GitHub Actions**: `.github/workflows/ci.yml`
- **Pipeline**: Build → Test → Docker Build
- **Triggers**: Push a main/develop, Pull Requests

## Decisiones arquitectónicas

1. **Clean Architecture**: Separación clara de responsabilidades
2. **Repository Pattern**: Abstracción de acceso a datos
3. **Dependency Injection**: Inversión de control nativa de .NET
4. **JWT Stateless**: Escalabilidad sin sesiones de servidor
5. **Bulk Operations**: Rendimiento en cargas masivas
6. **React SPA**: UX moderna con navegación fluida
7. **TypeScript**: Type safety en frontend
8. **Docker**: Consistencia entre entornos
