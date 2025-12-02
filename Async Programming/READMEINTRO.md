# Programación Asíncrona en C#

## Introducción

La programación asíncrona es una técnica en el desarrollo moderno que permite ejecutar operaciones de larga duración sin bloquear el hilo principal de la aplicación.

## Conceptos Clave

### Sincronía vs Asincronía

**Código Síncrono**: Ejecuta una tarea a la vez, bloqueando el hilo principal hasta completar
```csharp
string data = DescargarDatos();
ProcesarDatos(data);
```

**Código Asíncrono**: Permite continuar con otras tareas mientras se espera
```csharp
string data = await DescargarDatosAsync();
ProcesarDatos(data);
```

### Palabras Clave Principales

- **`async`**: Marca un método como asíncrono
- **`await`**: Espera la finalización de una tarea asíncrona sin bloquear
- **`Task`**: Representa una operación asíncrona que no devuelve valor
- **`Task<T>`**: Representa una operación asíncrona que devuelve un valor de tipo T

### Cuándo Usar Programación Asíncrona

✅ **Usar async cuando:**
- Accedes a archivos o bases de datos
- Haces llamadas a APIs web
- Realizas operaciones de red
- Procesas grandes cantidades de datos
- Interactúas con servicios externos

❌ **No usar async cuando:**
- Las operaciones son puramente CPU-bound (usar `Task.Run` en su lugar)
- El código es simple y se ejecuta instantáneamente
- Estás en un constructor (no soporta async)
