using SesoApi.Models;

namespace SesoApi.Services;

public class TemplateRegistry
{
    private static readonly List<TemplateDefinition> Templates =
    [
        // ─── Template 1: starter ───────────────────────────────────────────────
        new TemplateDefinition
        {
            Id = "starter",
            Name = "Starter",
            Description = "Página de aterrizaje básica para comenzar rápidamente.",
            Thumbnail = "/thumbnails/starter.png",
            Components =
            [
                new ComponentDefinition
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "logo", Type = "image", Label = "Logo" },
                        new FieldSchema { Key = "navLinks", Type = "list", Label = "Navegación",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["logo"] = "",
                        ["navLinks"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Inicio", ["url"] = "/" },
                            new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "hero",
                    Order = 2,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                        new FieldSchema { Key = "subtitle", Type = "textarea", Label = "Subtítulo" },
                        new FieldSchema { Key = "ctaText", Type = "text", Label = "Texto del botón" },
                        new FieldSchema { Key = "ctaLink", Type = "url", Label = "Link del botón" },
                        new FieldSchema { Key = "backgroundImage", Type = "image", Label = "Imagen de fondo" }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Bienvenido",
                        ["subtitle"] = "Tu negocio en línea",
                        ["ctaText"] = "Empezar",
                        ["ctaLink"] = "/app",
                        ["backgroundImage"] = ""
                    }
                },
                new ComponentDefinition
                {
                    Type = "features",
                    Order = 3,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Características",
                            ItemFields =
                            [
                                new FieldSchema { Key = "icon", Type = "select", Label = "Icono", Options = ["rocket", "shield", "chart", "star", "zap", "heart", "users", "mail", "edit", "globe"] },
                                new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                                new FieldSchema { Key = "description", Type = "textarea", Label = "Descripción" }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Características",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["icon"] = "rocket", ["title"] = "Rápido", ["description"] = "Despliegue en minutos" },
                            new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Seguro", ["description"] = "Protección total" },
                            new Dictionary<string, object> { ["icon"] = "chart", ["title"] = "Analytics", ["description"] = "Datos en tiempo real" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "text", Type = "text", Label = "Texto del footer" },
                        new FieldSchema
                        {
                            Key = "links", Type = "list", Label = "Enlaces",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026. Todos los derechos reservados.",
                        ["links"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" },
                            new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" }
                        }
                    }
                }
            ]
        },

        // ─── Template 2: business ──────────────────────────────────────────────
        new TemplateDefinition
        {
            Id = "business",
            Name = "Business",
            Description = "Plantilla corporativa completa con testimonios, precios y llamada a la acción.",
            Thumbnail = "/thumbnails/business.png",
            Components =
            [
                new ComponentDefinition
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "logo", Type = "image", Label = "Logo" },
                        new FieldSchema { Key = "navLinks", Type = "list", Label = "Navegación",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["logo"] = "",
                        ["navLinks"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Inicio", ["url"] = "/" },
                            new Dictionary<string, object> { ["label"] = "Servicios", ["url"] = "/services" },
                            new Dictionary<string, object> { ["label"] = "Precios", ["url"] = "/pricing" },
                            new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "hero",
                    Order = 2,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                        new FieldSchema { Key = "subtitle", Type = "textarea", Label = "Subtítulo" },
                        new FieldSchema { Key = "ctaText", Type = "text", Label = "Texto del botón" },
                        new FieldSchema { Key = "ctaLink", Type = "url", Label = "Link del botón" },
                        new FieldSchema { Key = "backgroundImage", Type = "image", Label = "Imagen de fondo" }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Soluciones para tu empresa",
                        ["subtitle"] = "La plataforma que impulsa el crecimiento de tu negocio",
                        ["ctaText"] = "Solicitar demo",
                        ["ctaLink"] = "/demo",
                        ["backgroundImage"] = ""
                    }
                },
                new ComponentDefinition
                {
                    Type = "features",
                    Order = 3,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Características",
                            ItemFields =
                            [
                                new FieldSchema { Key = "icon", Type = "select", Label = "Icono", Options = ["rocket", "shield", "chart", "star", "zap", "heart", "users", "mail", "edit", "globe"] },
                                new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                                new FieldSchema { Key = "description", Type = "textarea", Label = "Descripción" }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Por qué elegirnos",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["icon"] = "rocket", ["title"] = "Alto rendimiento", ["description"] = "Velocidad y escalabilidad garantizadas" },
                            new Dictionary<string, object> { ["icon"] = "shield", ["title"] = "Seguridad", ["description"] = "Tus datos siempre protegidos" },
                            new Dictionary<string, object> { ["icon"] = "users", ["title"] = "Soporte 24/7", ["description"] = "Equipo dedicado siempre disponible" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "testimonials",
                    Order = 4,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Testimonios",
                            ItemFields =
                            [
                                new FieldSchema { Key = "name", Type = "text", Label = "Nombre", Required = true },
                                new FieldSchema { Key = "role", Type = "text", Label = "Cargo / Empresa" },
                                new FieldSchema { Key = "avatar", Type = "image", Label = "Avatar" },
                                new FieldSchema { Key = "text", Type = "textarea", Label = "Testimonio", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Lo que dicen nuestros clientes",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["name"] = "María García", ["role"] = "CEO, TechStart", ["avatar"] = "", ["text"] = "La mejor inversión que hemos hecho. Resultados increíbles desde el primer mes." },
                            new Dictionary<string, object> { ["name"] = "Carlos López", ["role"] = "Director, Innova", ["avatar"] = "", ["text"] = "Soporte excepcional y plataforma muy intuitiva." }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "pricing",
                    Order = 5,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Planes",
                            ItemFields =
                            [
                                new FieldSchema { Key = "name", Type = "text", Label = "Nombre del plan", Required = true },
                                new FieldSchema { Key = "price", Type = "text", Label = "Precio", Required = true },
                                new FieldSchema { Key = "period", Type = "text", Label = "Período (ej: /mes)" },
                                new FieldSchema { Key = "description", Type = "textarea", Label = "Descripción" },
                                new FieldSchema { Key = "ctaText", Type = "text", Label = "Texto del botón" },
                                new FieldSchema { Key = "ctaLink", Type = "url", Label = "Link del botón" },
                                new FieldSchema { Key = "featured", Type = "toggle", Label = "Destacado" },
                                new FieldSchema { Key = "features", Type = "list", Label = "Funcionalidades",
                                    ItemFields =
                                    [
                                        new FieldSchema { Key = "text", Type = "text", Label = "Funcionalidad", Required = true }
                                    ]
                                }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Planes y precios",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object>
                            {
                                ["name"] = "Básico", ["price"] = "€29", ["period"] = "/mes",
                                ["description"] = "Perfecto para empezar",
                                ["ctaText"] = "Empezar", ["ctaLink"] = "/signup?plan=basic", ["featured"] = false,
                                ["features"] = new List<object>
                                {
                                    new Dictionary<string, object> { ["text"] = "Hasta 1.000 usuarios" },
                                    new Dictionary<string, object> { ["text"] = "Soporte por email" }
                                }
                            },
                            new Dictionary<string, object>
                            {
                                ["name"] = "Pro", ["price"] = "€79", ["period"] = "/mes",
                                ["description"] = "Para equipos en crecimiento",
                                ["ctaText"] = "Empezar", ["ctaLink"] = "/signup?plan=pro", ["featured"] = true,
                                ["features"] = new List<object>
                                {
                                    new Dictionary<string, object> { ["text"] = "Usuarios ilimitados" },
                                    new Dictionary<string, object> { ["text"] = "Soporte prioritario 24/7" },
                                    new Dictionary<string, object> { ["text"] = "Analytics avanzado" }
                                }
                            }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "cta-banner",
                    Order = 6,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                        new FieldSchema { Key = "subtitle", Type = "textarea", Label = "Subtítulo" },
                        new FieldSchema { Key = "ctaText", Type = "text", Label = "Texto del botón", Required = true },
                        new FieldSchema { Key = "ctaLink", Type = "url", Label = "Link del botón", Required = true },
                        new FieldSchema { Key = "backgroundColor", Type = "color", Label = "Color de fondo" }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "¿Listo para empezar?",
                        ["subtitle"] = "Únete a miles de empresas que ya confían en nosotros.",
                        ["ctaText"] = "Comenzar gratis",
                        ["ctaLink"] = "/signup",
                        ["backgroundColor"] = "#3B82F6"
                    }
                },
                new ComponentDefinition
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "text", Type = "text", Label = "Texto del footer" },
                        new FieldSchema
                        {
                            Key = "links", Type = "list", Label = "Enlaces",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026. Todos los derechos reservados.",
                        ["links"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" },
                            new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" },
                            new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" }
                        }
                    }
                }
            ]
        },

        // ─── Template 3: portfolio ─────────────────────────────────────────────
        new TemplateDefinition
        {
            Id = "portfolio",
            Name = "Portfolio",
            Description = "Plantilla para freelancers y creativos con galería, habilidades y formulario de contacto.",
            Thumbnail = "/thumbnails/portfolio.png",
            Components =
            [
                new ComponentDefinition
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "logo", Type = "image", Label = "Logo" },
                        new FieldSchema { Key = "navLinks", Type = "list", Label = "Navegación",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["logo"] = "",
                        ["navLinks"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Inicio", ["url"] = "/" },
                            new Dictionary<string, object> { ["label"] = "Proyectos", ["url"] = "/projects" },
                            new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "hero",
                    Order = 2,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                        new FieldSchema { Key = "subtitle", Type = "textarea", Label = "Subtítulo" },
                        new FieldSchema { Key = "ctaText", Type = "text", Label = "Texto del botón" },
                        new FieldSchema { Key = "ctaLink", Type = "url", Label = "Link del botón" },
                        new FieldSchema { Key = "backgroundImage", Type = "image", Label = "Imagen de fondo" }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Hola, soy creativo",
                        ["subtitle"] = "Diseñador y desarrollador freelance especializado en experiencias digitales únicas",
                        ["ctaText"] = "Ver proyectos",
                        ["ctaLink"] = "#gallery",
                        ["backgroundImage"] = ""
                    }
                },
                new ComponentDefinition
                {
                    Type = "gallery",
                    Order = 3,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Imágenes",
                            ItemFields =
                            [
                                new FieldSchema { Key = "image", Type = "image", Label = "Imagen", Required = true },
                                new FieldSchema { Key = "caption", Type = "text", Label = "Descripción" },
                                new FieldSchema { Key = "link", Type = "url", Label = "Link del proyecto" }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Proyectos",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["image"] = "", ["caption"] = "Proyecto 1", ["link"] = "" },
                            new Dictionary<string, object> { ["image"] = "", ["caption"] = "Proyecto 2", ["link"] = "" },
                            new Dictionary<string, object> { ["image"] = "", ["caption"] = "Proyecto 3", ["link"] = "" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "skills",
                    Order = 4,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Habilidades",
                            ItemFields =
                            [
                                new FieldSchema { Key = "name", Type = "text", Label = "Habilidad", Required = true },
                                new FieldSchema { Key = "level", Type = "number", Label = "Nivel (0-100)", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Mis habilidades",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["name"] = "Diseño UI/UX", ["level"] = 90 },
                            new Dictionary<string, object> { ["name"] = "Desarrollo Web", ["level"] = 85 },
                            new Dictionary<string, object> { ["name"] = "Fotografía", ["level"] = 75 }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "contact-form",
                    Order = 5,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título del formulario" },
                        new FieldSchema { Key = "subtitle", Type = "textarea", Label = "Subtítulo" },
                        new FieldSchema { Key = "email", Type = "text", Label = "Email de destino", Required = true, Placeholder = "tu@email.com" },
                        new FieldSchema { Key = "nameLabel", Type = "text", Label = "Etiqueta campo nombre" },
                        new FieldSchema { Key = "emailLabel", Type = "text", Label = "Etiqueta campo email" },
                        new FieldSchema { Key = "messageLabel", Type = "text", Label = "Etiqueta campo mensaje" },
                        new FieldSchema { Key = "submitText", Type = "text", Label = "Texto del botón enviar" }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Hablemos",
                        ["subtitle"] = "¿Tienes un proyecto en mente? Me encantaría escucharte.",
                        ["email"] = "",
                        ["nameLabel"] = "Tu nombre",
                        ["emailLabel"] = "Tu email",
                        ["messageLabel"] = "Tu mensaje",
                        ["submitText"] = "Enviar mensaje"
                    }
                },
                new ComponentDefinition
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "text", Type = "text", Label = "Texto del footer" },
                        new FieldSchema
                        {
                            Key = "links", Type = "list", Label = "Enlaces",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026. Todos los derechos reservados.",
                        ["links"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "LinkedIn", ["url"] = "https://linkedin.com" },
                            new Dictionary<string, object> { ["label"] = "GitHub", ["url"] = "https://github.com" }
                        }
                    }
                }
            ]
        },

        // ─── Template 4: ecommerce ─────────────────────────────────────────────
        new TemplateDefinition
        {
            Id = "ecommerce",
            Name = "E-commerce",
            Description = "Plantilla para tiendas en línea con slider, catálogo de productos y sellos de confianza.",
            Thumbnail = "/thumbnails/ecommerce.png",
            Components =
            [
                new ComponentDefinition
                {
                    Type = "header",
                    Order = 1,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "logo", Type = "image", Label = "Logo" },
                        new FieldSchema { Key = "navLinks", Type = "list", Label = "Navegación",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["logo"] = "",
                        ["navLinks"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Inicio", ["url"] = "/" },
                            new Dictionary<string, object> { ["label"] = "Tienda", ["url"] = "/shop" },
                            new Dictionary<string, object> { ["label"] = "Ofertas", ["url"] = "/deals" },
                            new Dictionary<string, object> { ["label"] = "Contacto", ["url"] = "/contact" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "hero-slider",
                    Order = 2,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema
                        {
                            Key = "slides", Type = "list", Label = "Diapositivas",
                            ItemFields =
                            [
                                new FieldSchema { Key = "title", Type = "text", Label = "Título", Required = true },
                                new FieldSchema { Key = "subtitle", Type = "textarea", Label = "Subtítulo" },
                                new FieldSchema { Key = "image", Type = "image", Label = "Imagen", Required = true },
                                new FieldSchema { Key = "ctaText", Type = "text", Label = "Texto del botón" },
                                new FieldSchema { Key = "ctaLink", Type = "url", Label = "Link del botón" }
                            ]
                        },
                        new FieldSchema { Key = "autoplay", Type = "toggle", Label = "Reproducción automática" },
                        new FieldSchema { Key = "interval", Type = "number", Label = "Intervalo (ms)" }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["slides"] = new List<object>
                        {
                            new Dictionary<string, object> { ["title"] = "Nueva temporada", ["subtitle"] = "Descubre las últimas novedades", ["image"] = "", ["ctaText"] = "Ver colección", ["ctaLink"] = "/shop/new" },
                            new Dictionary<string, object> { ["title"] = "Ofertas especiales", ["subtitle"] = "Hasta un 50% de descuento", ["image"] = "", ["ctaText"] = "Ver ofertas", ["ctaLink"] = "/deals" }
                        },
                        ["autoplay"] = true,
                        ["interval"] = 5000
                    }
                },
                new ComponentDefinition
                {
                    Type = "product-grid",
                    Order = 3,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Productos",
                            ItemFields =
                            [
                                new FieldSchema { Key = "name", Type = "text", Label = "Nombre del producto", Required = true },
                                new FieldSchema { Key = "price", Type = "text", Label = "Precio", Required = true },
                                new FieldSchema { Key = "image", Type = "image", Label = "Imagen del producto" },
                                new FieldSchema { Key = "link", Type = "url", Label = "Link al producto" },
                                new FieldSchema { Key = "badge", Type = "text", Label = "Badge (ej: Nuevo, Oferta)" }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Productos destacados",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["name"] = "Producto 1", ["price"] = "€29.99", ["image"] = "", ["link"] = "/product/1", ["badge"] = "Nuevo" },
                            new Dictionary<string, object> { ["name"] = "Producto 2", ["price"] = "€49.99", ["image"] = "", ["link"] = "/product/2", ["badge"] = "" },
                            new Dictionary<string, object> { ["name"] = "Producto 3", ["price"] = "€19.99", ["image"] = "", ["link"] = "/product/3", ["badge"] = "Oferta" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "trust-badges",
                    Order = 4,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Sellos de confianza",
                            ItemFields =
                            [
                                new FieldSchema { Key = "icon", Type = "select", Label = "Icono", Options = ["shield", "truck", "refresh", "star", "lock", "heart", "check", "zap"] },
                                new FieldSchema { Key = "text", Type = "text", Label = "Texto", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "¿Por qué comprarnos?",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["icon"] = "truck", ["text"] = "Envío gratis +€50" },
                            new Dictionary<string, object> { ["icon"] = "refresh", ["text"] = "Devoluciones en 30 días" },
                            new Dictionary<string, object> { ["icon"] = "lock", ["text"] = "Pago seguro" },
                            new Dictionary<string, object> { ["icon"] = "star", ["text"] = "Calidad garantizada" }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "testimonials",
                    Order = 5,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "title", Type = "text", Label = "Título de sección" },
                        new FieldSchema
                        {
                            Key = "items", Type = "list", Label = "Testimonios",
                            ItemFields =
                            [
                                new FieldSchema { Key = "name", Type = "text", Label = "Nombre", Required = true },
                                new FieldSchema { Key = "role", Type = "text", Label = "Cargo / Empresa" },
                                new FieldSchema { Key = "avatar", Type = "image", Label = "Avatar" },
                                new FieldSchema { Key = "text", Type = "textarea", Label = "Testimonio", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["title"] = "Lo que dicen nuestros clientes",
                        ["items"] = new List<object>
                        {
                            new Dictionary<string, object> { ["name"] = "Ana Martínez", ["role"] = "Cliente frecuente", ["avatar"] = "", ["text"] = "¡Productos de excelente calidad y envío rapidísimo!" },
                            new Dictionary<string, object> { ["name"] = "Pedro Sánchez", ["role"] = "Comprador verificado", ["avatar"] = "", ["text"] = "Muy satisfecho con mi compra, repetiré sin duda." }
                        }
                    }
                },
                new ComponentDefinition
                {
                    Type = "footer",
                    Order = 10,
                    Visible = true,
                    Schema =
                    [
                        new FieldSchema { Key = "text", Type = "text", Label = "Texto del footer" },
                        new FieldSchema
                        {
                            Key = "links", Type = "list", Label = "Enlaces",
                            ItemFields =
                            [
                                new FieldSchema { Key = "label", Type = "text", Label = "Texto", Required = true },
                                new FieldSchema { Key = "url", Type = "url", Label = "URL", Required = true }
                            ]
                        }
                    ],
                    DefaultData = new Dictionary<string, object>
                    {
                        ["text"] = "© 2026. Todos los derechos reservados.",
                        ["links"] = new List<object>
                        {
                            new Dictionary<string, object> { ["label"] = "Términos", ["url"] = "/terms" },
                            new Dictionary<string, object> { ["label"] = "Privacidad", ["url"] = "/privacy" },
                            new Dictionary<string, object> { ["label"] = "Envíos", ["url"] = "/shipping" },
                            new Dictionary<string, object> { ["label"] = "Devoluciones", ["url"] = "/returns" }
                        }
                    }
                }
            ]
        }
    ];

    public List<TemplateDefinition> GetAll() => Templates;

    public TemplateDefinition? GetById(string id) =>
        Templates.FirstOrDefault(t => string.Equals(t.Id, id, StringComparison.OrdinalIgnoreCase));

    public List<TenantComponent> GetDefaultComponents(string templateId)
    {
        var template = GetById(templateId);
        if (template is null)
            return [];

        return template.Components.Select(c => new TenantComponent
        {
            Type = c.Type,
            Order = c.Order,
            Visible = c.Visible,
            Data = new Dictionary<string, object>(c.DefaultData)
        }).ToList();
    }
}
