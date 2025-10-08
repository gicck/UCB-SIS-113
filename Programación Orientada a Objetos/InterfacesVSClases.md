# Interfaces vs Clases Abstractas: Casos de Uso Reales

## INTERFACES - Casos de Uso Reales
**"QUÉ PUEDES HACER" (Capacidades)**   
Las interfaces definen capacidades que pueden tener objetos de diferentes familias.

```c#
// ========================================
// USANDO INTERFACES (Correcto)
// ========================================

public interface IProcesadorPago
{
    bool ProcesarPago(decimal monto, string tarjeta);
    bool ReembolsarPago(string transaccionId);
    string ObtenerEstadoTransaccion(string transaccionId);
}

// Diferentes implementaciones SIN relación entre sí
public class PayPalProcesador : IProcesadorPago
{
    private PayPalAPI api;
    
    public bool ProcesarPago(decimal monto, string tarjeta)
    {
        // Lógica específica de PayPal
        return api.CreatePayment(monto);
    }
    
    public bool ReembolsarPago(string transaccionId)
    {
        return api.RefundPayment(transaccionId);
    }
    
    public string ObtenerEstadoTransaccion(string transaccionId)
    {
        return api.GetStatus(transaccionId);
    }
}

public class StripeProcesador : IProcesadorPago
{
    private StripeService service;
    
    public bool ProcesarPago(decimal monto, string tarjeta)
    {
        // Lógica completamente diferente de Stripe
        var charge = service.Charge(new ChargeOptions 
        { 
            Amount = monto * 100 // Stripe usa centavos
        });
        return charge.Status == "succeeded";
    }
    
    public bool ReembolsarPago(string transaccionId)
    {
        return service.Refund(transaccionId);
    }
    
    public string ObtenerEstadoTransaccion(string transaccionId)
    {
        return service.GetCharge(transaccionId).Status;
    }
}

public class MercadoPagoProcesador : IProcesadorPago
{
    private MPService mpService;
    
    public bool ProcesarPago(decimal monto, string tarjeta)
    {
        // Lógica de MercadoPago
        return mpService.CrearPreferencia(monto);
    }
    
    // ... resto de implementación
}

// ========================================
// USO POLIMÓRFICO
// ========================================

public class CarritoCompras
{
    private IProcesadorPago procesador;
    
    public CarritoCompras(IProcesadorPago procesador)
    {
        this.procesador = procesador; // ¡Inyección de dependencia!
    }
    
    public void RealizarCompra(decimal total, string tarjeta)
    {
        // No importa cuál procesador sea, todos cumplen el contrato
        if (procesador.ProcesarPago(total, tarjeta))
        {
            Console.WriteLine("✅ Pago exitoso");
        }
        else
        {
            Console.WriteLine("❌ Pago rechazado");
        }
    }
}

// En uso:
var carritoPayPal = new CarritoCompras(new PayPalProcesador());
var carritoStripe = new CarritoCompras(new StripeProcesador());
```

- PayPal, Stripe y MercadoPago NO comparten código
- Son proveedores completamente diferentes
- Solo necesitas garantizar que todos puedan procesar pagos
- Puedes cambiar entre ellos fácilmente (Strategy Pattern)



### ASP.NET Core - Inyección de Dependencias

```c#
// ========================================
// Framework: ASP.NET Core
// ========================================

// Interface del framework
public interface ILogger
{
    void LogInformation(string message);
    void LogError(string message, Exception ex);
    void LogWarning(string message);
}

// Múltiples implementaciones de diferentes librerías
public class ConsoleLogger : ILogger
{
    public void LogInformation(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }
    
    public void LogError(string message, Exception ex)
    {
        Console.WriteLine($"[ERROR] {message}: {ex.Message}");
    }
    
    public void LogWarning(string message)
    {
        Console.WriteLine($"[WARN] {message}");
    }
}

public class FileLogger : ILogger
{
    private string rutaArchivo;
    
    public void LogInformation(string message)
    {
        File.AppendAllText(rutaArchivo, $"[INFO] {message}\n");
    }
    // ... resto
}

public class DatabaseLogger : ILogger
{
    private DbContext context;
    
    public void LogInformation(string message)
    {
        context.Logs.Add(new Log { Nivel = "INFO", Mensaje = message });
        context.SaveChanges();
    }
    // ... resto
}

// En tu aplicación
public class UsuarioController
{
    private readonly ILogger logger;
    
    public UsuarioController(ILogger logger) // Inyección de dependencia
    {
        this.logger = logger;
    }
    
    public void CrearUsuario(Usuario usuario)
    {
        logger.LogInformation($"Creando usuario: {usuario.Nombre}");
        // ... lógica
    }
}
```
- Puedes cambiar el logger sin modificar el código del controller
- En desarrollo usas ConsoleLogger
- En producción usas DatabaseLogger
- En tests usas un MockLogger

### Entity Framework - IQueryable


## CLASES ABSTRACTAS - Casos de Uso Reales

**"QUÉ ERES" (Familia/Jerarquía)**   
Las clases abstractas definen una familia de objetos relacionados que comparten código.

### System.IO.Stream (Framework .NET)

```c#
// ========================================
// Framework: .NET Base Class Library
// ========================================

// Clase abstracta del framework
public abstract class Stream : IDisposable
{
    // Propiedades abstractas - cada tipo de stream las implementa diferente
    public abstract bool CanRead { get; }
    public abstract bool CanWrite { get; }
    public abstract bool CanSeek { get; }
    public abstract long Length { get; }
    public abstract long Position { get; set; }
    
    // Métodos abstractos - obligatorios
    public abstract int Read(byte[] buffer, int offset, int count);
    public abstract void Write(byte[] buffer, int offset, int count);
    public abstract long Seek(long offset, SeekOrigin origin);
    public abstract void Flush();
    
    // Método IMPLEMENTADO - código compartido por todos los streams
    public virtual void CopyTo(Stream destination)
    {
        byte[] buffer = new byte[81920];
        int bytesRead;
        while ((bytesRead = Read(buffer, 0, buffer.Length)) > 0)
        {
            destination.Write(buffer, 0, bytesRead);
        }
    }
    
    // Otro método implementado - todos los streams lo usan igual
    public void Dispose()
    {
        Close();
    }
    
    protected virtual void Close()
    {
        // Código de limpieza común
    }
}

// Implementaciones concretas del framework
public class FileStream : Stream
{
    private SafeFileHandle handle;
    
    public override bool CanRead => true;
    public override bool CanWrite => true;
    // ... implementación específica de archivos
    
    public override int Read(byte[] buffer, int offset, int count)
    {
        // Lectura directa del sistema operativo
        return ReadFile(handle, buffer, offset, count);
    }
}

public class MemoryStream : Stream
{
    private byte[] buffer;
    private int position;
    
    public override bool CanRead => true;
    public override bool CanWrite => true;
    // ... implementación en memoria
    
    public override int Read(byte[] buffer, int offset, int count)
    {
        // Lectura del array en memoria
        Array.Copy(this.buffer, position, buffer, offset, count);
        position += count;
        return count;
    }
}

public class NetworkStream : Stream
{
    private Socket socket;
    
    public override int Read(byte[] buffer, int offset, int count)
    {
        // Lectura desde la red
        return socket.Receive(buffer, offset, count, SocketFlags.None);
    }
}

// ========================================
// USO POLIMÓRFICO
// ========================================

public void ProcesarArchivo(Stream stream) // ¡Acepta CUALQUIER tipo de stream!
{
    byte[] buffer = new byte[1024];
    int bytesLeidos = stream.Read(buffer, 0, buffer.Length);
    
    // Usa el método compartido CopyTo
    using (var otroStream = new MemoryStream())
    {
        stream.CopyTo(otroStream); // Funciona para TODOS los streams
    }
}

// Puedes usar con cualquier tipo
ProcesarArchivo(new FileStream("archivo.txt", FileMode.Open));
ProcesarArchivo(new MemoryStream());
ProcesarArchivo(new NetworkStream(socket));
```
- Todos los streams comparten código común (CopyTo, Dispose)
- Existe una jerarquía clara: "todos SON streams"
- El código compartido evita duplicación
- Una interfaz obligaría a duplicar CopyTo en cada 
implementación

### ASP.NET Core - ControllerBase

```c#
// ========================================
// Framework: ASP.NET Core MVC
// ========================================

// Clase abstracta del framework
public abstract class ControllerBase
{
    // Propiedades compartidas - todos los controllers las tienen
    public HttpContext HttpContext { get; set; }
    public HttpRequest Request => HttpContext.Request;
    public HttpResponse Response => HttpContext.Response;
    public ClaimsPrincipal User => HttpContext.User;
    
    // Métodos helper compartidos - código reutilizable
    protected OkObjectResult Ok(object value)
    {
        return new OkObjectResult(value);
    }
    
    protected NotFoundObjectResult NotFound(object value)
    {
        return new NotFoundObjectResult(value);
    }
    
    protected BadRequestObjectResult BadRequest(object error)
    {
        return new BadRequestObjectResult(error);
    }
    
    protected CreatedAtActionResult CreatedAtAction(string actionName, object value)
    {
        return new CreatedAtActionResult(actionName, null, null, value);
    }
    
    // Método virtual - puedes personalizar
    protected virtual void OnActionExecuting()
    {
        // Hook antes de ejecutar acción
    }
}

// TU controller específico
[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService productoService;
    
    public ProductosController(IProductoService productoService)
    {
        this.productoService = productoService;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var productos = productoService.ObtenerTodos();
        return Ok(productos); // Usa método de la clase base
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var producto = productoService.ObtenerPorId(id);
        
        if (producto == null)
            return NotFound(new { mensaje = "Producto no encontrado" }); // Clase base
        
        return Ok(producto); // Clase base
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Producto producto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState); // Clase base
        
        productoService.Crear(producto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto); // Clase base
    }
}
```

- TODOS los controllers necesitan Ok(), NotFound(), BadRequest()
- Evita duplicar estos métodos en cada controller
- Provee acceso consistente a HttpContext, User, etc.
- Con interfaz tendrías que implementar todo esto 100 veces

### Entity Framework Core - DbContext


## Cuando usar que

### Usa INTERFAZ cuando:
```c#
// Múltiples proveedores sin relación
IProcesadorPago → PayPal, Stripe, MercadoPago

// Capacidades opcionales (herencia múltiple)
class Perro : IAnimal, IEntrenable, IRastreable { }

// Inyección de dependencias
public class Service(ILogger logger, IRepository repo) { }

// Testing/Mocking
public interface IEmailService { }
public class MockEmailService : IEmailService { } // Para tests
```

### Usa CLASE ABSTRACTA cuando:

```c#
// Familia de objetos relacionados con código compartido
abstract class Stream → FileStream, MemoryStream, NetworkStream

// Template Method Pattern (flujo común)
abstract class Notificacion con método Enviar() que llama pasos específicos

// Jerarquía de herencia clara
abstract class Empleado → EmpleadoTiempoCompleto, EmpleadoContratista

// Frameworks que proveen funcionalidad base
abstract class DbContext, ControllerBase, etc.
```