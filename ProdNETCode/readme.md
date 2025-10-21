# Guía de Referencia Rápida - Principios de Diseño en C#
## Hoja de Referencia de Principios de Diseño

### 1. Cohesión y SRP (Principio de Responsabilidad Única)
**Una clase, un trabajo**
- Cada clase debe tener solo una razón para cambiar
- Alta cohesión = funcionalidad relacionada agrupada
- Ejemplo: Separar UserRepository, EmailService, Validator en clases distintas

**Pregúntate:** "¿Puedo describir el propósito de esta clase en una oración sin usar 'y'?"

---

### 2. Encapsulación y Abstracción
**Oculta los detalles, muestra la interfaz**
- Mantén el estado interno privado
- Expón comportamiento, no datos
- Usa propiedades para acceso controlado
- Abstrae la complejidad

```csharp
// ✅ Bueno: Encapsulación con propiedades
public class Cuenta
{
    private decimal _saldo;
    
    public decimal Saldo 
    { 
        get => _saldo;
        private set => _saldo = value >= 0 ? value : 0;
    }
    
    public void Depositar(decimal cantidad) 
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser positiva");
        _saldo += cantidad;
    }
}
```

**Pregúntate:** "Si cambio esta implementación interna, ¿se romperá el código cliente?"

---

### 3. Acoplamiento Débil y Modularidad
**Depende de abstracciones, no de implementaciones concretas**
- Usa interfaces y clases abstractas
- Inyecta dependencias (no las instancies internamente)
- Los componentes deben ser intercambiables
- Minimiza las dependencias entre módulos

```csharp
// ✅ Bueno: Inyección de dependencias
public interface IEmailService
{
    Task EnviarCorreoAsync(string destinatario, string mensaje);
}

public class NotificacionService
{
    private readonly IEmailService _emailService;
    
    public NotificacionService(IEmailService emailService)
    {
        _emailService = emailService;
    }
}

// Configuración en Program.cs (.NET 6+)
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<NotificacionService>();
```

**Pregúntate:** "¿Puedo hacer pruebas unitarias de este componente sin instanciar la mitad del sistema?"

---

### 4. Reusabilidad y Extensibilidad
**Abierto para extensión, cerrado para modificación**
- Usa composición sobre herencia
- Patrón Strategy para variaciones
- Arquitecturas de plugins
- No hardcodees comportamiento

```csharp
// ✅ Bueno: Patrón Strategy
public interface IDescuentoStrategy
{
    decimal CalcularDescuento(decimal precio);
}

public class DescuentoNavidad : IDescuentoStrategy
{
    public decimal CalcularDescuento(decimal precio) => precio * 0.25m;
}

public class Pedido
{
    private readonly IDescuentoStrategy _descuento;
    
    public Pedido(IDescuentoStrategy descuento)
    {
        _descuento = descuento;
    }
}
```

**Pregúntate:** "¿Puedo agregar nueva funcionalidad sin editar código existente?"

---

### 5. Portabilidad
**Escribe una vez, ejecuta en cualquier lugar**
- Usa .NET Core/.NET 5+ (multiplataforma)
- Path.Combine en lugar de concatenación de rutas
- Variables de entorno para configuración
- Evita suposiciones específicas de plataforma

```csharp
// ✅ Bueno: Multiplataforma
string rutaArchivo = Path.Combine("datos", "archivo.txt");

// Configuración con appsettings.json y variables de entorno
var configuracion = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

// ❌ Malo: Específico de Windows
string rutaArchivo = "datos\\archivo.txt";
```

**Pregúntate:** "¿Funcionará esto en Linux, Windows y Mac?"

---

### 6. Defensibilidad
**Falla rápido, falla seguro, falla ruidosamente**

**Fail-fast**: Valida la entrada inmediatamente
```csharp
public void ProcesarPago(decimal cantidad)
{
    if (cantidad <= 0)
        throw new ArgumentException("La cantidad debe ser positiva", nameof(cantidad));
    
    // Procesar...
}
```

**Menor privilegio**: Almacena/registra solo lo necesario
```csharp
// Almacenar: "****-****-****-1234" 
// NO: Número completo de tarjeta de crédito
public string OcultarNumeroTarjeta(string numero)
{
    return $"****-****-****-{numero[^4..]}";
}
```

**Valores por defecto seguros**: Valores conservadores y seguros
```csharp
public class ConfiguracionApp
{
    public bool ModoDebug { get; set; } = false;  // ¡No true!
    public int TimeoutSegundos { get; set; } = 30;  // ¡No null!
    public string EntornoEjecucion { get; set; } = "Production";
}
```

**Pregúntate:** "¿Qué es lo peor que podría pasar con entrada incorrecta?"

---

### 7. Mantenibilidad y Testabilidad
- Escribe código claro y autodocumentado
- Funciones puras cuando sea posible
- Separa la lógica de negocio del I/O
- Escribe pruebas (Tests)

**Ejemplo de función pura:**
```csharp
// Pura: misma entrada = misma salida
public int Sumar(int a, int b) => a + b;

// Impura: efectos secundarios
public int SumarYRegistrar(int a, int b)
{
    var resultado = a + b;
    Console.WriteLine($"Resultado: {resultado}");  // ¡Efecto secundario!
    return resultado;
}
```

**Pregúntate:** "¿Puedo escribir una prueba unitaria sin mockear 5 cosas?"

---

### 8. Simplicidad (KISS, DRY, YAGNI)

**KISS - Keep It Simple, Stupid (Mantenlo Simple)**
- Prefiere soluciones simples sobre las ingeniosas
- Evita abstracciones innecesarias
- Si es difícil de explicar, es demasiado complejo

**DRY - Don't Repeat Yourself (No Te Repitas)**
- Extrae lógica común en métodos
- Única fuente de verdad
- Repetición = carga de mantenimiento

**YAGNI - You Aren't Gonna Need It (No Lo Vas a Necesitar)**
- No construyas para necesidades futuras hipotéticas
- Agrega características cuando realmente se necesiten
- Resiste la sobre-ingeniería

**Pregúntate:** 
- "¿Estoy haciendo esto más complejo de lo necesario?"
- "¿He escrito esta lógica exacta en otro lugar?"
- "¿Realmente necesitaré esta característica?"

---

## Olores de Código Comunes vs Soluciones

| Olor de Código | Principio Violado | Solución |
|----------------|-------------------|----------|
| Clase Dios haciendo todo | SRP | Dividir en clases enfocadas |
| Campos públicos en todos lados | Encapsulación | Usar campos privados + propiedades |
| Dependencias hardcodeadas | Acoplamiento Débil | Inyección de dependencias |
| If/else gigante para tipos | Extensibilidad | Strategy/polimorfismo |
| Rutas hardcodeadas | Portabilidad | Archivos config + Path.Combine |
| Fallos silenciosos | Defensibilidad | Fail-fast con excepciones |
| Método de 500 líneas | Mantenibilidad | Dividir en métodos pequeños |
| Código copy-paste | DRY | Extraer a método |
| Código "futuro" sin usar | YAGNI | ¡Bórralo! |

---
