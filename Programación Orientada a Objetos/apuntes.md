# 26/08

# C# OOP Abstraction Practice Exercises

## Ejercicio 1: Sistema Bancario

### Desafío de Programación Secuencial
Crea un programa de consola que gestione cuentas bancarias utilizando variables y funciones básicas:

**Requirements:**
- Usa arreglos para almacenar números de cuenta, nombres de titulares y saldos
- Crea funciones para: CreateAccount(), Deposit(), Withdraw(), CheckBalance()
- Maneja múltiples cuentas utilizando bucles y sentencias condicionales
- Muestra la información de las cuentas usando una salida simple en consola

**Sample Sequential Approach:**
```csharp
// Global arrays
// ...
static void CreateAccount(string name, decimal initialBalance) 
{
    // Add to arrays, increment counter
}

static void Deposit(int accountNumber, decimal amount) 
{
    // Find account in array, update balance
}
```

### Conversion a POO (Abstraccion Applicada)

## Ejercicio 2: Sistema de Gestión de Biblioteca

### Desafío de Programación Secuencial
Construye un sistema de biblioteca utilizando construcciones básicas de programación:

Requisitos:

- Usa arreglos paralelos para títulos de libros, autores, números ISBN y estado de disponibilidad
- Crea funciones: AddBook(), BorrowBook(), ReturnBook(), SearchBook()
- Utiliza bucles para buscar entre los libros
- Lleva el control de libros prestados/disponibles con arreglos booleanos

**Sample Sequential Approach:**
```csharp
// variables
static void AddBook(string title, string author, string isbn)
{
    // Add to parallel arrays
}

static int FindBookIndex(string title)
{
    // Loop through array to find book
}
```

### OOP Conversion (Abstraction Applied)

```csharp
// Abstract representation of a Book
public class Book
{
    // Private fields - hiding internal structure
    private string _title;
    private string _author;
    private string _isbn;
    private bool _isAvailable;

    public Book(string title, string author, string isbn)
    {
        _title = title;
        _author = author;
        _isbn = isbn;
        _isAvailable = true; // New books are available
    }

    // Simple interface for book operations
    public bool Borrow()
    {
        if (_isAvailable)
        {
            _isAvailable = false;
            Console.WriteLine($"'{_title}' has been borrowed.");
            return true;
        }
        Console.WriteLine($"'{_title}' is not available.");
        return false;
    }

    public void Return()
    {
        _isAvailable = true;
        Console.WriteLine($"'{_title}' has been returned.");
    }

    public void DisplayInfo()
    {
        string status = _isAvailable ? "Available" : "Borrowed";
        Console.WriteLine($"'{_title}' by {_author} | ISBN: {_isbn} | Status: {status}");
    }

    // Properties for controlled access
    public string Title => _title;
    public string Author => _author;
    public bool IsAvailable => _isAvailable;
}

// Library class managing the book collection
public class Library
{
    private List<Book> _books = new List<Book>();

    public void AddBook(string title, string author, string isbn)
    {
        var book = new Book(title, author, isbn);
        _books.Add(book);
        Console.WriteLine($"Added '{title}' to library.");
    }

    public Book SearchBook(string title)
    {
        return _books.FirstOrDefault(book => 
            book.Title.ToLower().Contains(title.ToLower()));
    }

    public void DisplayAllBooks()
    {
        Console.WriteLine("Library Catalog:");
        foreach (var book in _books)
        {
            book.DisplayInfo();
        }
    }

    public void BorrowBook(string title)
    {
        var book = SearchBook(title);
        book?.Borrow();
    }

    public void ReturnBook(string title)
    {
        var book = SearchBook(title);
        book?.Return();
    }
}
```

**Beneficios de la Abstracción:**

- El estado interno del libro está oculto y se gestiona internamente
- Los usuarios interactúan a través de métodos simples: Borrow(), Return(), DisplayInfo()
- La complejidad de la búsqueda está abstraída dentro de la clase Library
- No es necesario gestionar manualmente los arreglos paralelos

---

### Aplicacion de Herencia

Tu clase Libro existente funciona bien, pero la biblioteca ahora necesita gestionar diferentes tipos de medios con comportamientos especializados:

#### Requisitos extendidos:

**Múltiples tipos de medios:** libros, DVD y revistas.

**Diferentes reglas de préstamo:** cada tipo de medio tiene diferentes períodos de préstamo.

**Propiedades especializadas:** los DVD tienen directores y duración, las revistas tienen números de edición, etc.

**Tarifas por mora variadas:** diferentes estructuras de penalización para cada tipo de medio.

**Formatos de visualización:** cada tipo de medio muestra información diferente.

### Paso 1: Identificar qué debe heredarse

Al observar tu clase Libro actual, identifica los comportamientos comunes que compartirán todos los tipos de medios:
 - Título, estado de disponibilidad
 - Métodos `Borrow()` y `Return()`
 - Funcionalidad básica de visualización

Y los comportamientos especializados que diferirán:

 - Períodos de préstamo (libros: 14 días, DVD: 7 días, revistas: 3 días)
 - Cálculos de tarifas por mora
 - Propiedades específicas (autor vs. director vs. número de edición)

---

## Ejercicio 3: Gestión de Calificaciones de Estudiantes

### Desafío de Programación Secuencial

Crea un sistema de calificaciones estudiantiles utilizando programación procedural:

**Requisitos:**

- Usa arreglos para almacenar nombres de estudiantes, materias y calificaciones
- Funciones para: AddStudent(), AddGrade(), CalculateAverage(), DisplayReport()
- Utiliza bucles anidados para manejar múltiples materias por estudiante
- Calcula el GPA utilizando sentencias condicionales

**Sample Sequential Approach:**
```csharp
// variables

static void AddStudent(string name) { /* Add to arrays */ }
static double CalculateAverage(int studentIndex) { /* Loop and calculate */ }
```

### OOP Conversion (Abstraction Applied)

```csharp
// Abstract representation of a grade for a subject
public class Grade
{
    public string Subject { get; private set; }
    public double Score { get; private set; }

    public Grade(string subject, double score)
    {
        Subject = subject;
        Score = Math.Max(0, Math.Min(100, score)); // Ensure valid range
    }
}

// Abstract representation of a Student
public class Student
{
    private string _name;
    private List<Grade> _grades = new List<Grade>();

    public Student(string name)
    {
        _name = name;
    }

    // Simple interface for grade operations
    public void AddGrade(string subject, double score)
    {
        var grade = new Grade(subject, score);
        _grades.Add(grade);
        Console.WriteLine($"Added grade {score} for {subject} to {_name}");
    }

    // Abstracted calculation - user doesn't need to know the formula
    public double CalculateAverage()
    {
        if (_grades.Count == 0) return 0;
        return _grades.Average(g => g.Score);
    }

    public string GetLetterGrade()
    {
        double avg = CalculateAverage();
        return avg switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }

    public void DisplayReport()
    {
        Console.WriteLine($"\n--- Report for {_name} ---");
        foreach (var grade in _grades)
        {
            Console.WriteLine($"{grade.Subject}: {grade.Score}");
        }
        Console.WriteLine($"Average: {CalculateAverage():F2}");
        Console.WriteLine($"Letter Grade: {GetLetterGrade()}");
    }

    public string Name => _name;
    public int GradeCount => _grades.Count;
}

// Class to manage multiple students
public class GradeBook
{
    private List<Student> _students = new List<Student>();

    public Student AddStudent(string name)
    {
        var student = new Student(name);
        _students.Add(student);
        Console.WriteLine($"Student '{name}' added to gradebook.");
        return student;
    }

    public Student FindStudent(string name)
    {
        return _students.FirstOrDefault(s => 
            s.Name.ToLower() == name.ToLower());
    }

    public void DisplayClassReport()
    {
        Console.WriteLine("=== CLASS REPORT ===");
        foreach (var student in _students)
        {
            student.DisplayReport();
        }
    }
}
```
**Beneficios de la Abstracción:**

- El cálculo complejo del promedio está oculto dentro de los métodos
- La lógica de conversión a calificación en letras está abstraída
- Los estudiantes no necesitan saber cómo se almacenan internamente las calificaciones
- Interfaz limpia: AddGrade(), CalculateAverage(), DisplayReport()
---

### Aplicacion de Herencia

Tu clase Estudiante actual funciona para escenarios básicos, pero la escuela ahora necesita manejar diferentes categorías de estudiantes con reglas especializadas:

#### Requisitos extendidos:
- Múltiples tipos de estudiantes: pregrado, posgrado y estudiantes de intercambio
- Diferentes cálculos de GPA: los de pregrado usan la escala de 4.0, los de posgrado tienen cursos ponderados
- Requisitos de graduación: diferentes requisitos de créditos y GPA según el tipo de estudiante
- Propiedades especializadas: los de posgrado tienen requisitos de tesis, los de intercambio tienen universidades de origen

#### Paso 1: Identificar oportunidades de herencia

De tu clase Estudiante actual, extrae:

Comportamientos comunes:
- Nombre, número de estudiante (ID)
- Agregar calificaciones
- Almacenamiento básico de calificaciones

Comportamientos especializados:

- Métodos de cálculo de GPA - Diferente implementacion para cadaa tipo de estudiante
- Reglas de elegibilidad para graduación 
- Determinación de la situación académica 
---

## Ejercicio 4: Sistema de Gestión de Inventario

### Desafío de Programación Secuencial
Construye un sistema de inventario para una pequeña tienda:

**Requisitos:**
- Usa arreglos para nombres de productos, cantidades, precios y categorías
- Funciones: AddProduct(), UpdateQuantity(), CheckStock(), CalculateTotal()
- Utiliza bucles para buscar y actualizar el inventario
- Calcula el valor total del inventario utilizando aritmética básica

**Sample Sequential Approach:**
```csharp
// variables

static void AddProduct(string name, int qty, decimal price, string category) { }
static int FindProductIndex(string name) { /* Linear search */ }
static decimal CalculateTotalValue() { /* Loop through all products */ }
```

### OOP Conversion (Abstraction Applied)

```csharp
// Abstract representation of a Product
public class Product
{
    private string _name;
    private int _quantity;
    private decimal _price;
    private string _category;

    public Product(string name, int quantity, decimal price, string category)
    {
        _name = name;
        _quantity = Math.Max(0, quantity); // Ensure non-negative
        _price = Math.Max(0, price);       // Ensure non-negative
        _category = category;
    }

    // Simple operations on the product
    public bool RemoveStock(int amount)
    {
        if (amount <= _quantity && amount > 0)
        {
            _quantity -= amount;
            Console.WriteLine($"Removed {amount} units of {_name}. Remaining: {_quantity}");
            return true;
        }
        Console.WriteLine($"Cannot remove {amount} units. Available: {_quantity}");
        return false;
    }

    public void AddStock(int amount)
    {
        if (amount > 0)
        {
            _quantity += amount;
            Console.WriteLine($"Added {amount} units of {_name}. New quantity: {_quantity}");
        }
    }

    public decimal GetTotalValue()
    {
        return _quantity * _price;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"{_name} | Category: {_category} | Qty: {_quantity} | Price: ${_price} | Total Value: ${GetTotalValue()}");
    }

    public bool IsLowStock(int threshold = 5)
    {
        return _quantity <= threshold;
    }

    // Properties for controlled access
    public string Name => _name;
    public int Quantity => _quantity;
    public decimal Price => _price;
    public string Category => _category;
}

// Inventory management system
public class Inventory
{
    private List<Product> _products = new List<Product>();

    public void AddProduct(string name, int quantity, decimal price, string category)
    {
        var product = new Product(name, quantity, price, category);
        _products.Add(product);
        Console.WriteLine($"Product '{name}' added to inventory.");
    }

    public Product FindProduct(string name)
    {
        return _products.FirstOrDefault(p => 
            p.Name.ToLower() == name.ToLower());
    }

    public decimal CalculateTotalInventoryValue()
    {
        return _products.Sum(p => p.GetTotalValue());
    }

    public void DisplayLowStockItems(int threshold = 5)
    {
        Console.WriteLine($"Low Stock Items (≤{threshold} units):");
        var lowStockItems = _products.Where(p => p.IsLowStock(threshold));
        
        foreach (var product in lowStockItems)
        {
            product.DisplayInfo();
        }
    }

    public void DisplayInventoryByCategory(string category)
    {
        Console.WriteLine($"Products in '{category}' category:");
        var categoryProducts = _products.Where(p => 
            p.Category.ToLower() == category.ToLower());
            
        foreach (var product in categoryProducts)
        {
            product.DisplayInfo();
        }
    }

    public void DisplayFullInventory()
    {
        Console.WriteLine("=== FULL INVENTORY ===");
        foreach (var product in _products)
        {
            product.DisplayInfo();
        }
        Console.WriteLine($"Total Inventory Value: ${CalculateTotalInventoryValue()}");
    }
}
```

**Beneficios de la Abstracción:**
- La lógica de validación del stock está oculta dentro de los métodos
- Los cálculos complejos de inventario están abstraídos
- Los usuarios interactúan a través de métodos simples: AddStock(), RemoveStock(), DisplayInfo()
- El mecanismo de almacenamiento interno está completamente oculto
- La lógica de negocio (como alertas de bajo stock) está encapsulada dentro de las clases

---


### Aplicacion de Herencia
La clase Producto actual maneja el inventario básico, pero la tienda ahora necesita categorías de productos especializadas con diferentes reglas de negocio:

Requisitos extendidos:

- Categorías de productos: perecederos, electrónicos, ropa
- Precios dinámicos: los perecederos obtienen descuentos cerca de la fecha de vencimiento, los electrónicos tienen ventas por temporada
- Propiedades especializadas: fechas de caducidad, períodos de garantía, tallas, etc.
- Cálculos de envío: artículos frágiles, según peso, envío exprés para perecederos

---

## Principios Clave de la Abstracción Demostrados

1. **Ocultar Detalles de Implementación**: Los usuarios no ven arreglos, bucles o cálculos complejos
2. **Proveer Interfaces Limpias**:  Nombres de métodos simples y significativos
3. **Encapsular Datos y Comportamientos Relacionados**: Todo lo relacionado con un concepto está en una sola clase
4. **Proteger la Integridad de los Datos**: Campos privados con acceso controlado a través de métodos
5. **Modelar Entidades del Mundo Real**: Las clases representan cosas reales (Account, Book, Student, Product)

## Tips

- Comienza con la versión secuencial para entender el problema
- Identifica las "cosas" (sustantivos) que deberían convertirse en clases
- Identifica las "acciones" (verbos) que deberían convertirse en métodos
- Pregunta: "¿Qué debería poder hacer el usuario?" (métodos públicos)
- Pregunta: "¿Qué debería estar oculto?" (campos/métodos privados)

# Semana 6 

**Tema:** Abstracción y Encapsulamiento  
**Objetivo:** Practicar la creación de clases que ocultan sus detalles internos y exponen interfaces claras mediante métodos públicos.  
**Nota:** No es necesario usar herencia ni polimorfismo en estos ejercicios.  

---

## Ejercicio 1: Sistema de Reservas de Cine  
Diseña un sistema para gestionar reservas de entradas en un cine.  
- Cada sala tiene un número limitado de asientos.  
- El sistema debe permitir reservar un asiento, cancelar una reserva y mostrar los asientos disponibles.  

Puntos clave:  
- El usuario **no debe manipular directamente** la lista de asientos.  
- La verificación de si un asiento está libre **debe estar encapsulada** en la clase correspondiente.  
- Interfaz pública clara (ej. `ReservarAsiento()`, `CancelarReserva()`, `MostrarDisponibles()`).  

---

## Ejercicio 2: Agenda de Contactos  
Diseña un sistema para manejar contactos dentro de una agenda digital.  
- Cada contacto cuenta con nombre, número de teléfono y correo electrónico.  
- El usuario debe poder agregar contactos, buscar un contacto específico y mostrar todos los contactos almacenados.  

Puntos clave:  
- La lista interna de contactos **no es accesible directamente** por el usuario.  
- Se deben usar métodos como `AgregarContacto()`, `BuscarContacto()`, `MostrarContactos()`.  
- El encapsulamiento asegura que los contactos solo pueden gestionarse mediante los métodos públicos definidos.  

--- 

## Ejercicio 3: Sistema de Control de Estacionamiento

Diseña un sistema que gestione un estacionamiento para automóviles.
- Cada vehículo tiene placa, modelo y hora de entrada.
- El sistema debe permitir registrar la entrada de un vehículo, registrar su salida y calcular el monto a pagar según el tiempo de estacionamiento.

Puntos clave:
- Los datos internos como hora de entrada y tarifa por hora deben estar encapsulados y no pueden modificarse directamente.
- La lógica de cálculo del monto a pagar (basada en las horas) debe estar oculta dentro de un método como `CalcularPago()`.
- El usuario solo debe interactuar con métodos públicos como:
	- `RegistrarEntrada()`, `RegistrarSalida()`, `MostrarVehiculos()`.

---

1. **Fase de Diseño sin código:**   
2. **Dinámica por Equipos:**  
3. **Iteración de Mejoras:**  
4. **Role Play "Usuarios vs. Diseñadores":**  


# Semana 9

Polimorfismo en C#
Objetivo: Aplicar polimorfismo para extender los sistemas desarrollados permitiendo múltiples implementaciones de comportamientos comunes.

## Ejercicio: Agenda de Contactos con Polimorfismo
Contexto Previo
Requisitos Extendidos con Polimorfismo
La agenda ahora debe manejar:

- Contactos Personales: con fecha de cumpleaños y relación familiar
- Contactos Profesionales: con empresa, cargo y extensión
- Contactos de Emergencia: con tipo de emergencia y disponibilidad 24/7

**Comportamientos Polimórficos Requeridos:**

1. Método `MostrarInformacion()`: Cada tipo de contacto debe mostrar información diferente
2. Método `EsDisponible(DateTime hora)`:
  - Personales: solo en horario social (9 AM - 9 PM)
  - Profesionales: solo en horario laboral (8 AM - 6 PM)
  - Emergencia: disponible 24/7
3. Método `ObtenerPrioridad()`:
  - Emergencia: Alta
  - Profesional: Media
  - Personal: Baja

```c#
// Implementa la jerarquía de clases utilizando polimorfismo
public abstract class Contacto
{
    // Propiedades comunes
    protected string nombre;
    protected string telefono;
    protected string email;
    
    // Constructor base
    public Contacto(string nombre, string telefono, string email)
    {
        // ...implementar
    }
    
    // Métodos virtuales/abstractos para polimorfismo
    public abstract void MostrarInformacion();
    public abstract bool EsDisponible(DateTime hora);
    public abstract string ObtenerPrioridad();
}

public class ContactoPersonal : Contacto
{
    private DateTime fechaCumpleanos;
    private string relacion;
    
    // Implementar constructor y métodos polimórficos
}

public class ContactoProfesional : Contacto
{
    private string empresa;
    private string cargo;
    private int extension;
    
    // Implementar constructor y métodos polimórficos
}

public class ContactoEmergencia : Contacto
{
    private string tipoEmergencia;
    
    // Implementar constructor y métodos polimórficos
}

public class Agenda
{
    private List<Contacto> contactos = new List<Contacto>();
    
    // Métodos que trabajen con polimorfismo
    public void MostrarContactosDisponibles(DateTime hora)
    {
        // Usar polimorfismo para verificar disponibilidad
    }
    
    public void MostrarContactosPorPrioridad()
    {
        // Usar polimorfismo para ordenar por prioridad
    }
}
```

| Situación | Usa... | Ejemplo |
|-----------|--------|---------|
| La clase base puede existir por sí sola | Herencia simple | Empleado → EmpleadoTiempoCompleto | 
| La clase base es demasiado genérica | Clase abstracta | Figura → Circulo, Cuadrado | 
| Solo necesitas un contrato | Interfaz | IComparable, IDisposable |
| Quieres compartir código común | Clase abstracta | Interfaz con esteroides | 
| Necesitas herencia múltiple | Interfaces | C# no permite herencia múltiple |



Agrega a tu agenda este sistema de etiquetas:

```csharp
// ¿Debería ser abstracta o concreta? ¡TÚ decides!
public ??? class Etiqueta
{
    public string Nombre { get; set; }
    public string Color { get; set; }
    
    // ¿Virtual, abstracto o normal?
    public ??? void Aplicar(Contacto contacto)
    {
        // ¿Qué va aquí?
    }
}

public class EtiquetaFavorito : Etiqueta
{
    // Contactos favoritos tienen estrella dorada
}

public class EtiquetaBloqueado : Etiqueta
{
    // Contactos bloqueados no pueden llamar
}
```

Preguntas para reflexionar:

- ¿Tiene sentido crear una "etiqueta" genérica sin tipo específico?
- ¿Todos los tipos de etiqueta se aplican igual?
- ¿Necesitas código compartido o solo un contrato?




## Ejercicio: Sistema de Transporte Urbano

### Contexto
Una empresa de transporte urbano necesita un sistema para gestionar diferentes tipos de vehículos de transporte público con tarifas, capacidades y reglas operativas específicas.

Requisitos Base con Herencia
**Tipos de Vehículos:**

- Autobús Urbano: capacidad 40 pasajeros, tarifa fija, rutas específicas
- Metrobus: capacidad 120 pasajeros, tarifa por distancia, vías exclusivas
- Taxi: capacidad 4 pasajeros, tarifa por tiempo + distancia, servicio directo
- Bicicleta Pública: capacidad 1 persona, tarifa por minutos, estaciones fijas

Comportamientos Polimórficos Requeridos:

1. `CalcularTarifa(double distancia, int tiempoMinutos)`:
  - Autobús: tarifa fija $15 independiente de distancia/tiempo
  - Metrobus: $8 base + $2 por cada 5km
  - Taxi: $25 base + $3 por minuto + $1.50 por km
  - Bicicleta: $5 por cada 15 minutos o fracción

2. `VerificarDisponibilidad(DateTime momento):`
  - Autobús: 5:00 AM - 11:00 PM
  - Metrobus: 6:00 AM - 10:00 PM
  - Taxi: 24/7
  - Bicicleta: 6:00 AM - 8:00 PM

3. `ObtenerCapacidadDisponible():`
  - Cada tipo maneja ocupación diferente
  - Retorna espacios libres actuales

4. `MostrarInformacionViaje():`
  - Cada tipo muestra información específica del servicio


Implementación Requerida:
```c#
public abstract class VehiculoTransporte
{
    protected string identificador;
    protected int capacidadMaxima;
    protected int pasajerosActuales;
    protected bool enServicio;
    
    public VehiculoTransporte(string identificador, int capacidadMaxima)
    {
        // Implementar constructor base
    }
    
    // Métodos abstractos para polimorfismo
    public abstract decimal CalcularTarifa(double distanciaKm, int tiempoMinutos);
    public abstract bool VerificarDisponibilidad(DateTime momento);
    public abstract void MostrarInformacionViaje();
    
    // Métodos virtuales que pueden ser sobrescritos
    public virtual bool SubirPasajeros(int cantidad)
    {
        // Implementación base para validar capacidad
    }
    
    public virtual bool BajarPasajeros(int cantidad)
    {
        // Implementación base
    }
    
    public int ObtenerCapacidadDisponible()
    {
        return capacidadMaxima - pasajerosActuales;
    }
}

// Implementar las clases derivadas:
public class AutobusUrbano : VehiculoTransporte { }
public class Metrobus : VehiculoTransporte { }
public class TaxiUrbano : VehiculoTransporte { }
public class BicicletaPublica : VehiculoTransporte { }

public class SistemaTransporte
{
    private List<VehiculoTransporte> flota = new List<VehiculoTransporte>();
    
    public void CalcularMejorOpcion(double distancia, int tiempo, DateTime momento)
    {
        // Usar polimorfismo para encontrar la opción más económica disponible
    }
    
    public void MostrarVehiculosDisponibles(DateTime momento)
    {
        // Mostrar solo vehículos disponibles en el momento dado
    }
    
    public void GenerarReporteFlota()
    {
        // Usar polimorfismo para mostrar información de toda la flota
    }
}
```

#### DETONANTE

"La alcaldía cambió la ley: TODOS los vehículos ahora deben registrar cada viaje en una base de datos para estadísticas de movilidad"

PROBLEMA:

- Tienen que agregar método RegistrarViaje() en Autobús 
- Tienen que agregar método RegistrarViaje() en Taxi 
- Mañana agregarán Metrobus... tendrán que agregar otra vez 
- Si cambia el formato del registro, modificar 3+ clases

"¿Cómo podríamos escribir el código de RegistrarViaje() UNA SOLA VEZ y que todos lo hereden?"


#### Refactorización a Clase Abstracta

PREGUNTA: "¿Qué cosas pueden ir en la clase base y cuáles deben ser abstractas?"

### Capacidades Opcionales

"La ciudad quiere integración con apps de mapas. ALGUNOS vehículos pueden ser rastreados en tiempo real (GPS), otros no."


| Vehículo     | GPS     | WiFi | Reservar | Pagar App |
| ------------|---------|-------|--------- |----------|
| Autobus      |   ❌    |  ✅  |    ❌    |    ❌    |
| Taxi         |   ✅    |  ✅  |    ✅    |    ✅    |
| Metrobus     |   ✅    |  ❌  |    ❌    |    ❌    |
| Bicicleta    |   ✅    |  ❌  |    ✅    |    ✅    |

```c#
public abstract class VehiculoTransporte
{
    public abstract Ubicacion ObtenerUbicacionGPS(); // ❌ Autobus no tiene GPS
    public abstract void ReservarViaje(); // ❌ Metrobus no se reserva
}
```

- Autobus tendría que implementar ObtenerUbicacionGPS() ¡cuando no tiene GPS!

- Metrobus tendría que implementar ReservarViaje() ¡cuando no se reserva!

"¿Cómo modelamos capacidades que solo ALGUNOS tienen?"

--------
"Todos los vehículos necesitan mantenimiento periódico, pero cada tipo tiene intervalos diferentes"

"Algunos vehículos ahora son eléctricos y necesitan recarga. El Metrobus siempre fue eléctrico, pero ahora también hay Taxis eléctricos y Bicicletas eléctricas"

"Algunos vehículos tienen rampa para sillas de ruedas"
