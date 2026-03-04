# seso-api

Backend API del proyecto SESO — plataforma SaaS multi-tenant.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)

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

- **Swagger UI**: http://localhost:5000/swagger
- **OpenAPI JSON**: http://localhost:5000/swagger/v1/swagger.json

## Sistema Draft / Publish

El editor del dashboard puede guardar cambios temporales como **borrador** sin afectar la landing pública. El preview de Astro puede leer esos borradores para renderizar SSR puro.

### Flujo de trabajo

1. **Editar** → React hace `PUT /api/tenant/config?tenant=acme` para guardar borrador
2. **Preview** → El iframe de Astro carga `/preview?tenant=acme&draft=true`, que hace `GET /api/tenant/config?tenant=acme&draft=true`
3. **Publicar** → React hace `POST /api/tenant/config/publish?tenant=acme` para publicar el borrador
4. **Descartar** → React hace `DELETE /api/tenant/config/draft?tenant=acme` para volver al estado publicado

## Endpoints

### Health

```bash
# Estado de la API
curl http://localhost:5000/api/health
```

### Tenants

```bash
# Listar todos los tenants
curl http://localhost:5000/api/tenants

# Obtener configuración PUBLICADA de un tenant
curl "http://localhost:5000/api/tenant/config?tenant=acme"

# Obtener BORRADOR (o publicada si no hay borrador)
curl "http://localhost:5000/api/tenant/config?tenant=acme&draft=true"

# Obtener configuración usando header X-Tenant
curl http://localhost:5000/api/tenant/config -H "X-Tenant: acme"

# Guardar como borrador (no afecta la landing pública)
curl -X PUT "http://localhost:5000/api/tenant/config?tenant=acme" \
  -H "Content-Type: application/json" \
  -d '{"primaryColor": "#FF0000"}'

# Guardar y publicar directamente
curl -X PUT "http://localhost:5000/api/tenant/config?tenant=acme&publish=true" \
  -H "Content-Type: application/json" \
  -d '{"primaryColor": "#FF0000"}'

# Publicar el borrador actual (copia draft → published, elimina draft)
curl -X POST "http://localhost:5000/api/tenant/config/publish?tenant=acme"

# Descartar el borrador (vuelve al estado publicado)
curl -X DELETE "http://localhost:5000/api/tenant/config/draft?tenant=acme"

# Verificar si hay un borrador pendiente
curl "http://localhost:5000/api/tenant/config/has-draft?tenant=acme"
# Respuesta: { "hasDraft": true }

# Obtener módulos de un tenant
curl "http://localhost:5000/api/tenant/modules?tenant=acme"
```

### Referencia de endpoints

| Método | Ruta | Descripción |
|--------|------|-------------|
| `GET` | `/api/tenant/config?tenant={slug}` | Devuelve config publicada |
| `GET` | `/api/tenant/config?tenant={slug}&draft=true` | Devuelve borrador (o publicada si no hay borrador) |
| `PUT` | `/api/tenant/config?tenant={slug}` | Guarda como borrador |
| `PUT` | `/api/tenant/config?tenant={slug}&publish=true` | Guarda y publica directamente |
| `POST` | `/api/tenant/config/publish?tenant={slug}` | Publica el borrador actual |
| `DELETE` | `/api/tenant/config/draft?tenant={slug}` | Descarta el borrador |
| `GET` | `/api/tenant/config/has-draft?tenant={slug}` | Devuelve `{ "hasDraft": bool }` |
| `GET` | `/api/tenant/modules?tenant={slug}` | Obtiene módulos del tenant |
| `GET` | `/api/tenants` | Lista todos los tenants |

## Respuesta con `hasDraft`

El GET de configuración incluye el campo `hasDraft` que indica si hay cambios sin publicar:

```json
{
  "slug": "acme",
  "name": "Acme Corp",
  "hasDraft": true,
  "primaryColor": "#3B82F6",
  ...
}
```

## Tenants precargados

| Slug      | Nombre     | Colores           | Template    |
|-----------|------------|-------------------|-------------|
| `default` | SESO       | Rojos / oscuros   | default     |
| `acme`    | Acme Corp  | Azules            | corporate   |
| `globex`  | Globex     | Verdes            | nature      |

## Estructura del proyecto

```
seso-api/
├── Program.cs                   # Configuración de la app (CORS, Swagger, middleware, endpoints)
├── appsettings.json
├── appsettings.Development.json # Puerto 5000
├── Properties/
│   └── launchSettings.json      # Perfil de ejecución
├── Models/
│   ├── TenantConfig.cs          # Configuración visual del tenant (incluye HasDraft)
│   ├── TenantModule.cs          # Módulos contratados
│   └── UpdateTenantRequest.cs   # DTO para actualizaciones parciales
├── Services/
│   └── TenantStore.cs           # In-memory store con soporte draft/publish
├── Endpoints/
│   ├── HealthEndpoints.cs       # GET /api/health
│   └── TenantEndpoints.cs       # Endpoints de tenant con soporte draft/publish
├── Middleware/
│   └── TenantMiddleware.cs      # Detecta tenant del header X-Tenant o query param ?tenant=
└── seso-api.csproj
```

## Notas importantes

- **Los datos están en memoria**: al reiniciar la API, todos los cambios se pierden y los tenants vuelven a su estado inicial (seed data).
- **Sin autenticación**: esta API está pensada para desarrollo local y entornos controlados.
- **CORS configurado** para cualquier origen `localhost`.
- **Detección de tenant**: se puede pasar via query param `?tenant=slug` o header `X-Tenant: slug`.
- **PUT con actualizaciones parciales**: solo los campos incluidos en el body se actualizan; el resto se conserva.
- **Cambio de plantilla inteligente**: al cambiar de template, los componentes se fusionan conservando los datos existentes y agregando nuevos con valores por defecto.
