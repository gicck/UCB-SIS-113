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

### OOP Conversion (Abstraction Applied)


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

