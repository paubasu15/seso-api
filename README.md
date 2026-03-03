# seso-api

Backend API del proyecto SESO — plataforma SaaS multi-tenant.

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Cómo ejecutar

```bash
dotnet run
```

La API arranca en **http://localhost:5000** (en modo Development).

Para producción:
```bash
dotnet run --environment Production
```

## Documentación interactiva

Con el proyecto corriendo en Development, accede a:

- **Scalar UI**: http://localhost:5000/scalar/v1
- **OpenAPI JSON**: http://localhost:5000/openapi/v1.json

## Endpoints

### Health

```bash
# Estado de la API
curl http://localhost:5000/api/health
```

### Tenants

```bash
# Listar todos los tenants (development/testing)
curl http://localhost:5000/api/tenants

# Obtener configuración visual de un tenant
curl "http://localhost:5000/api/tenant/config?tenant=acme"

# Obtener configuración usando header X-Tenant
curl http://localhost:5000/api/tenant/config -H "X-Tenant: acme"

# Actualizar configuración (parcial o completa)
curl -X PUT "http://localhost:5000/api/tenant/config?tenant=acme" \
  -H "Content-Type: application/json" \
  -d '{"primaryColor": "#FF0000", "hero": {"title": "Nuevo título", "subtitle": "Subtítulo", "ctaText": "Ir", "ctaLink": "/app", "backgroundImage": ""}}'

# Obtener módulos de un tenant
curl "http://localhost:5000/api/tenant/modules?tenant=acme"
```

## Tenants precargados

| Slug      | Nombre     | Colores           |
|-----------|------------|-------------------|
| `default` | SESO       | Rojos / oscuros   |
| `acme`    | Acme Corp  | Azules            |
| `globex`  | Globex     | Verdes            |

## Estructura del proyecto

```
seso-api/
├── Program.cs                   # Configuración de la app (CORS, OpenAPI, middleware, endpoints)
├── appsettings.json
├── appsettings.Development.json # Puerto 5000 y orígenes permitidos
├── Models/
│   ├── TenantConfig.cs          # Configuración visual del tenant
│   ├── TenantModule.cs          # Módulos contratados
│   └── UpdateTenantRequest.cs   # DTO para actualizaciones parciales
├── Services/
│   └── TenantStore.cs           # In-memory store con 3 tenants de ejemplo
├── Endpoints/
│   ├── HealthEndpoints.cs       # GET /api/health
│   └── TenantEndpoints.cs       # GET/PUT /api/tenant/config, GET /api/tenant/modules, GET /api/tenants
├── Middleware/
│   └── TenantMiddleware.cs      # Detecta tenant del header X-Tenant o query param ?tenant=
└── seso-api.csproj
```

## Notas importantes

- **Los datos están en memoria**: al reiniciar la API, todos los cambios se pierden y los tenants vuelven a su estado inicial (seed data).
- **Sin autenticación**: esta API está pensada para desarrollo local y entornos controlados.
- **CORS configurado** para `localhost:4321` (Astro), `localhost:5173` (React/Vite) y `localhost:3000`.
- **Detección de tenant**: se puede pasar via query param `?tenant=slug` o header `X-Tenant: slug`.
- **PUT con actualizaciones parciales**: solo los campos incluidos en el body se actualizan; el resto se conserva.
