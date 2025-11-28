# Asisya - DEV II (Prueba técnica)

## Requisitos
- Docker & Docker Compose
- .NET 7 SDK
- Node 18+

## Run (Docker)
docker-compose up --build

- API: http://localhost:5000
- Frontend: http://localhost:3000

## Endpoints principales
- POST /api/Auth/login -> body: { username, password } (admin/password)
- POST /api/Category (Authorization required)
- POST /api/Product?count=100000 (Authorization required) -> genera N productos
- GET /api/Product?page=1&size=20
- GET /api/Product/{id}
- PUT /api/Product/{id} (Authorization required)
- DELETE /api/Product/{id} (Authorization required)

## Notas arquitectónicas
- Clean architecture: Domain / Application / Infrastructure / Api
- DTOs usados para salida
- Repositorios inyectados por DI
- JWT para auth
- Para cargas masivas en producción usar EFCore.BulkExtensions o COPY

## CI
- GitHub Actions en `.github/workflows/ci.yml` para build y tests
